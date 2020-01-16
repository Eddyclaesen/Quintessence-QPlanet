using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Quintessence.QJobService.Interfaces;
using Quintessence.QJobService.Scheduler.Data;
using Quintessence.QJobService.Scheduler.Model;

namespace Quintessence.QJobService.Scheduler
{
    public class JobControllerService : ServiceBase, IJobControllerService
    {
        private readonly static object PausedLock = new object();
        private readonly static object AgentsLock = new object();

        private bool _isPaused;
        private readonly static List<Thread> Agents = new List<Thread>();
        private FileInfo _servicePath;
        private Thread _mainThread;
        private Thread _jobCreatorThread;
        private Thread _historyCleaner;

        protected override void OnStart(string[] args)
        {
            _isPaused = false;
            _mainThread = new Thread(() =>
            {
                WriteWarning("JobController started.");

                _servicePath = new FileInfo(Assembly.GetExecutingAssembly().Location);

                StartJobCreator();
                StartHistoryCleaner();

                var errorWaitMinutes = 0;

                while (!_isPaused)
                {
                    try
                    {
                        var agent = RequestAgent(RunScheduledJob);
                        if (agent != null)
                            agent.Start();

                        errorWaitMinutes = 0;
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                    catch (Exception exception)
                    {
                        errorWaitMinutes++;

                        WriteError(string.Format("Error during JobScheduler. See details for more information. Retrying in {0} minutes.", errorWaitMinutes), exception);
                        Thread.Sleep(TimeSpan.FromMinutes(errorWaitMinutes));
                    }
                }
            });
            _mainThread.Start();
        }

        private void RunScheduledJob()
        {
            using (var context = new JobContext())
            {
                var job = context.Jobs.Where(j => j.StartDate == null).OrderBy(j => j.RequestDate).FirstOrDefault();

                if (job != null)
                {
                    WriteInformation(string.Format("Job {0}: Executing.", job.Id));

                    var jobDefinition = context.JobDefinitions.SingleOrDefault(jd => jd.Id == job.JobDefinitionId);

                    if (jobDefinition == null)
                    {
                        WriteEvent(string.Format("Job {0}: Unable to find jobdefinition with id {1}.", job.Id, job.JobDefinitionId), EventLogEntryType.Warning);
                        return;
                    }

                    WriteInformation(string.Format("Job {0}: {1}.", job.Id, jobDefinition.Name));

                    try
                    {
                        var assembly = Assembly.LoadFrom(_servicePath.DirectoryName + @"\" + jobDefinition.Assembly);

                        var instance = assembly.CreateInstance(jobDefinition.Class);

                        if (instance is IJobDefinition)
                        {
                            job.StartDate = DateTime.Now;
                            context.SaveChanges();

                            ((IJobDefinition)instance).Run(this);

                            job.EndDate = DateTime.Now;
                            job.Success = true;
                            context.SaveChanges();
                        }
                        else
                        {
                            job.Success = false;
                            context.SaveChanges();
                        }
                    }
                    catch (Exception exception)
                    {
                        WriteError(string.Format("Job {0}: Unable to execute job.", job.Id), exception);
                    }

                    WriteInformation(string.Format("Job {0}: Executed.", job.Id));
                }
            }
        }

        private void StartJobCreator()
        {
            _jobCreatorThread = new Thread(() =>
                {
                    WriteWarning("JobCreator started.");
                    while (!_isPaused)
                    {
                        Job latestJob = null;
                        try
                        {
                            using (var context = new JobContext())
                            {
                                var jobDefinitions = context.JobDefinitions.Where(jd => jd.IsEnabled).ToList();

                                foreach (var jobDefinition in jobDefinitions)
                                {
                                    var currentTimeOfDay = DateTime.Now.TimeOfDay;
                                    var jobDefinitionId = jobDefinition.Id;
                                    var jobSchedules = context.JobSchedules
                                                              .Where(js => js.JobDefinitionId == jobDefinitionId)
                                                              .Where(
                                                                  js =>
                                                                  js.StartTime <= currentTimeOfDay &&
                                                                  js.EndTime >= currentTimeOfDay).ToList();

                                    foreach (var jobSchedule in jobSchedules)
                                    {
                                        var jobScheduleId = jobSchedule.Id;

                                        latestJob = context.Jobs
                                                           .Where(
                                                               j =>
                                                               j.JobDefinitionId == jobDefinitionId &&
                                                               j.JobScheduleId == jobScheduleId)
                                                           .OrderByDescending(j => j.RequestDate)
                                                           .FirstOrDefault();

                                        if (latestJob == null ||
                                            latestJob.RequestDate.Add(TimeSpan.FromSeconds(jobSchedule.Interval)) <
                                            DateTime.Now)
                                        {
                                            WriteInformation(string.Format("JobCreator: Creating job for {0}.",
                                                                           jobDefinition.Name));

                                            latestJob = context.Jobs.Add(context.Jobs.Create());
                                            latestJob.Id = Guid.NewGuid();
                                            latestJob.JobDefinitionId = jobDefinitionId;
                                            latestJob.JobScheduleId = jobScheduleId;
                                            latestJob.RequestDate = DateTime.Now;
                                        }
                                    }
                                }

                                context.SaveChanges();

                                Thread.Sleep(TimeSpan.FromSeconds(latestJob != null ? 1 : 5));
                            }
                        }
                        
                        catch (Exception)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(30));
                        }
                    }
                });
            _jobCreatorThread.Start();
        }

        private void StartHistoryCleaner()
        {
            _historyCleaner = new Thread(() =>
            {
                WriteWarning("JobHistoryCleaner started.");
                while (!_isPaused)
                {
                    try
                    {
                        using (var context = new JobContext())
                        {
                            WriteInformation("JobHistoryCleaner: looking for jobs to remove.");

                            var jobDefinitions = context.JobDefinitions.ToList();

                            foreach (var jobDefinition in jobDefinitions)
                            {
                                var jobDefinitionId = jobDefinition.Id;
                                var jobs =
                                    context.Jobs.Where(
                                        j =>
                                        j.Success.HasValue && j.Success.Value && j.JobDefinitionId == jobDefinitionId)
                                           .OrderByDescending(j => j.EndDate)
                                           .Skip(GetNumberOfEntriesToKeep())
                                           .ToList();

                                foreach (
                                    var job in
                                        jobs.Where(
                                            job =>
                                            job.EndDate.HasValue &&
                                            job.EndDate.Value.Add(TimeSpan.FromMinutes(1)) < DateTime.Now))
                                    context.Jobs.Remove(job);
                            }
                            var result = context.SaveChanges();

                            WriteInformation(string.Format("JobHistoryCleaner: {0} jobs removed.", result));
                        }
                    }
                    catch (Exception exception)
                    {
                        WriteError("JobHistoryCleaner: Something went wrong", exception);
                    }
                    finally
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(30));
                    }
                }

            });
            _historyCleaner.Start();
        }

        protected override void OnPause()
        {
            lock (PausedLock)
                _isPaused = true;
            WriteWarning("JobScheduler paused.");
        }

        protected override void OnContinue()
        {
            lock (PausedLock)
                _isPaused = false;
            WriteWarning("JobScheduler resumed.");
        }

        protected override void OnStop()
        {
            WriteWarning("JobScheduler stop requested.");
            OnPause();
            int numberOfAgents;
            var numberOfAttempts = 0;
            while ((numberOfAgents = Agents.Count) > 0 && ++numberOfAttempts <= 10)
            {
                WriteWarning(string.Format("JobScheduler waiting to stop. Attempt {0}/10. {1} thread(s) processing.", numberOfAttempts, numberOfAgents));
                Thread.Sleep(10000);
            }

            Dispose(true);
            WriteWarning("JobScheduler stopped.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_jobCreatorThread != null)
                    _jobCreatorThread.Abort();
                _jobCreatorThread = null;

                if (_historyCleaner != null)
                    _historyCleaner.Abort();
                _historyCleaner = null;

                if (_mainThread != null)
                    _mainThread.Abort();
                _mainThread = null;
            }
            base.Dispose(disposing);
        }

        private Thread RequestAgent(Action action)
        {
            lock (AgentsLock)
            {
                var numberOfAgents = GetNumberOfAgents();
                if (Agents.Count < numberOfAgents)
                {
                    var thread = new Thread(() =>
                        {
                            try
                            {
                                action.Invoke();
                            }
                            finally
                            {
                                lock (AgentsLock)
                                {
                                    Agents.Remove(Thread.CurrentThread);
                                }
                            }
                        });
                    Agents.Add(thread);
                    return thread;
                }

                return null;
            }
        }

        private int GetNumberOfAgents()
        {
            var numberOfAgents = 4;
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["Agents"], out numberOfAgents);
                return numberOfAgents;
            }
            catch
            {
                return numberOfAgents;
            }
        }

        private int GetNumberOfEntriesToKeep()
        {
            var numberOfEntriesToKeep = 10;
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["JobHistoryEntries"], out numberOfEntriesToKeep);
                return numberOfEntriesToKeep;
            }
            catch
            {
                return numberOfEntriesToKeep;
            }
        }

        private EventLogEntryType GetEventLogThreshold()
        {
            var eventLogThreshold = EventLogEntryType.Warning;
            try
            {
                Enum.TryParse(ConfigurationManager.AppSettings["EventLogThreshold"], true, out eventLogThreshold);
                return eventLogThreshold;
            }
            catch
            {
                return eventLogThreshold;
            }
        }

        public void StartService()
        {
            OnStart(new string[0]);
        }

        public void WriteInformation(string message)
        {
            WriteEvent(message);
        }

        public void WriteError(string message, Exception exception = null)
        {
            WriteEvent(string.Format("{0}{1}{2}{1}{3}", message, Environment.NewLine, exception.Message, exception.StackTrace), EventLogEntryType.Error);
        }

        public void WriteWarning(string message)
        {
            WriteEvent(message, EventLogEntryType.Warning);
        }

#if DEBUG

        internal void StopService()
        {
            OnStop();
        }
#endif

        private void WriteEvent(string message, EventLogEntryType type = EventLogEntryType.Information)
        {
            if (type > GetEventLogThreshold())
                return;

            const string source = "Quintessence.QJobService.Scheduler";
            const string log = "Application";

            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);

            EventLog.WriteEntry(source, message, type, Thread.CurrentThread.ManagedThreadId);
            WriteConsole(message, type);
        }

        private static void WriteConsole(string message, EventLogEntryType type)
        {
            var originalForgroundColor = Console.ForegroundColor;

            switch (type)
            {
                case EventLogEntryType.FailureAudit:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case EventLogEntryType.SuccessAudit:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case EventLogEntryType.Information:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case EventLogEntryType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case EventLogEntryType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(message);
            Console.ForegroundColor = originalForgroundColor;
        }

        private void InitializeComponent()
        {
            // 
            // JobControllerService
            // 
            this.ServiceName = "QJobSchedulerService";

        }
    }
}