using System;
using System.Linq;
using System.Collections.Generic;

using Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.TeamLeader.Models;

namespace Quintessence.QJobService.JobDefinitions.TeamLeaderDuplication.DataAccess
{
    public class DuplicationDataAccess : IDuplicationDataAccess
    {
        public string RetrieveDuplicationSettingsByKey(string key, string defaultValue)
        {
            try
            {
                using (var dbCtx = new TeamLeaderEntities())
                {
                    DuplicationSetting setting = dbCtx.DuplicationSettings.FirstOrDefault(s => s.Key == key);
                    return (setting != null && !String.IsNullOrEmpty(setting.Value)) ? setting.Value : defaultValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing QuintEssence database", ex);
            }
        }

        public IDictionary<string, string> RetrieveDuplicationSettingsByKeys(IEnumerable<string> keys)
        {
            try
            {
                using (var dbCtx = new TeamLeaderEntities())
                {
                    return dbCtx.DuplicationSettings.Where(s => keys.Contains(s.Key)).ToDictionary(s => s.Key, s => s.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing TeamLeader database", ex);
            }
        }

        public void RegisterDuplicationError(DateTime logDateUtc, string info)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.DuplicationErrorLogs.Max(c => (int?)c.Id) ?? 0;
                maxId++;
                DuplicationErrorLog newDuplicationErrorLog = new DuplicationErrorLog
                {
                    Id = maxId,
                    LogDateUtc = logDateUtc,
                    Info = info
                };
                dbCtx.DuplicationErrorLogs.Add(newDuplicationErrorLog);
                dbCtx.SaveChanges();
            }
        }

        public Guid RegisterDuplicationJobHistory(string jobName, DateTime startDate, DateTime? endDate, bool succeeded, string info)
        {
            DuplicationJobHistory duplicationJobHistory = new DuplicationJobHistory
            {
                Id = Guid.NewGuid(),
                JobName = jobName,
                StartDate = startDate,
                EndDate = endDate,
                Succeeded = succeeded
            };

            using (var dbCtx = new TeamLeaderEntities())
            {
                dbCtx.DuplicationJobHistories.Add(duplicationJobHistory);
                dbCtx.SaveChanges();
            }
            return duplicationJobHistory.Id;   
        }

        public Guid RegisterDuplicationJobHistoryStart(string jobName)
        {
            return RegisterDuplicationJobHistory(jobName, DateTime.Now, null, false, null);

        }

        public void RegisterDuplicationJobHistoryEnd(Guid duplicationJobHistoryId, bool succeeded, string info)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                DuplicationJobHistory jobHistory = dbCtx.DuplicationJobHistories.FirstOrDefault(j => j.Id == duplicationJobHistoryId);
                if (jobHistory != null)
                {
                    jobHistory.EndDate = DateTime.Now;
                    jobHistory.Succeeded = succeeded;
                    jobHistory.Info = info;
                    dbCtx.SaveChanges();
                }
            }   
        }

        public void TruncateDuplicationTables()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                dbCtx.TruncateDuplicationTables();
            }
        }

        public int RegisterUsers(IEnumerable<TeamLeaderUser> users)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Users.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderUser u in users)
                {
                    maxId++;
                    User newUser = new User
                    {
                        Id = maxId,
                        TeamLeaderId = u.Id.GetValueOrDefault(),
                        Name = u.Name,
                        Email = u.Email,
                        Gsm = u.Gsm,
                        Telephone = u.Telephone,
                        Title = u.Title,

                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Users.Add(newUser);
                }
                return dbCtx.SaveChanges();
            } 
        }

        public int RegisterContacts(IEnumerable<TeamLeaderContact> contacts)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Contacts.Max(c => (int?) c.Id) ?? 0;               
                foreach(TeamLeaderContact c in contacts)
                {
                    maxId++;
                    Contact newContact = new Contact
                    {
                        Id = maxId,
                        TeamLeaderId = c.Id.GetValueOrDefault(),
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Street = c.Street,
                        Number = c.Number,
                        Zipcode = c.ZipCode,
                        City = c.City,
                        Country = c.Country,
                        Website = c.WebSite,
                        Email = c.Email,
                        Gsm = c.Gsm,
                        Telephone = c.Telephone,
                        Fax = c.Fax,
                        Gender = c.Gender,
                        LanguageCode = c.LanguageCode,
                        DateAdded = c.DateAdded,
                        DateEdited = c.DateEdited,
                        Status = c.Status,
                        LegacyId = c.LegacyId,
                        Positie = c.Positie,
	                    FormerEmployee = c.FormerEmployee,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Contacts.Add(newContact);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterCompanies(IEnumerable<TeamLeaderCompany> companies)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Companies.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderCompany c in companies)
                {
                    maxId++;
                    Company newCompany = new Company
                    {
                        Id = maxId,
                        TeamLeaderId = c.Id.GetValueOrDefault(),
                        Name = c.Name,
                        TaxCode = c.TaxCode,
                        BusinessType = c.BusinessType,
                        Street = c.Street,
                        Number = c.Number,
                        ZipCode = c.ZipCode,
                        City = c.City,
                        Country = c.Country,
                        Website = c.WebSite,
                        Email = c.Email,
                        Telephone = c.Telephone,
                        Fax = c.Fax,
                        Iban = c.Iban,
                        Bic = c.Bic,
                        LanguageCode = c.LanguageCode,
                        Afdeling = c.Afdeling,
                        AccountManagerTwo = c.AccountManagerTwo,
                        Assistent = c.Assistent,
                        DateAdded = c.DateAdded,
                        DateEdited = c.DateEdited,
                        Status = c.Status,
                        Business = c.Business,
                        FidelisatieNorm = c.FidelisatieNorm,
                        FocusList = c.FocusList.HasValue ? (c.FocusList.Value > 0) : (bool?)null,
                        LinkedInProfile = c.LinkedInProfile,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Companies.Add(newCompany);
                }
                return dbCtx.SaveChanges();
            } 
        }

        public int RegisterProjects(IEnumerable<TeamLeaderProject> projects)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Projects.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderProject p in projects)
                {
                    maxId++;
                    Project newProject = new Project
                    {
                        Id = maxId,
                        TeamLeaderId = p.Id.GetValueOrDefault(),
                        Title = p.Title,
                        Phase = p.Status,
                        StartDate = p.StartDate,
                        LegacyId = p.LegacyId,
                        BoekJaar = p.BoekJaar,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Projects.Add(newProject);
                }
                return dbCtx.SaveChanges();
            }            
        }

        public int RegisterTasks(IEnumerable<TeamLeaderTask> tasks)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Tasks.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderTask t in tasks)
                {
                    maxId++;
                    Task newTask = new Task
                    {
                        Id = maxId,
                        TeamLeaderId = t.Id.GetValueOrDefault(),
                        Description = t.Description,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        Intern = t.Intern,
                        Klant = t.Klant,
                        Overige = t.Overige,
                        ProjectId = t.ProjectId,
                        Sales = t.Sales,
                        Reservatie = t.Reservatie,
                        Code = t.Code,
                        LanguageCode = t.Language,
                        Gender = t.Gender,
                        IsFinished = t.IsFinished.GetValueOrDefault(),
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Tasks.Add(newTask);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterTasksIfNotExisting(IEnumerable<TeamLeaderTask> tasks)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Tasks.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderTask t in tasks)
                {
                    if(dbCtx.Tasks.Any(et => et.TeamLeaderId == t.Id))
                        continue;

                    maxId++;
                    Task newTask = new Task
                    {
                        Id = maxId,
                        TeamLeaderId = t.Id.GetValueOrDefault(),
                        Description = t.Description,
                        FirstName = t.FirstName,
                        LastName = t.LastName,
                        Intern = t.Intern,
                        Klant = t.Klant,
                        Overige = t.Overige,
                        ProjectId = t.ProjectId,
                        Sales = t.Sales,
                        Reservatie = t.Reservatie,
                        Code = t.Code,
                        LanguageCode = t.Language,
                        Gender = t.Gender,
                        IsFinished = t.IsFinished.GetValueOrDefault(),
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Tasks.Add(newTask);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterTimeTrackings(IEnumerable<TeamLeaderTimeTracking> timeTrackings)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.TimeTrackings.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderTimeTracking t in timeTrackings)
                {
                    maxId++;
                    TimeTracking newTimeTracking = new TimeTracking
                    {
                        Id = maxId,
                        TeamLeaderId = t.Id.GetValueOrDefault(),
                        ProjectId = t.ProjectId,
                        ProjectTitle = t.ProjectTitle,
                        UserId = t.UserId,
                        EmployeeName = t.EmployeeName,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        ContactId = t.ContactId,
                        ContactName = t.ContactName,
                        CompanyId = t.CompanyId,
                        CompanyName = t.CompanyName,
                        Description = t.Description,
                        Category = t.Category,
                        RelatedToObjectType = t.IsRelatedTo.TranslatedObjectType,
                        RelatedToObjectId = t.IsRelatedTo.ObjectId,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.TimeTrackings.Add(newTimeTracking);
                }
                return dbCtx.SaveChanges();
            } 
        }

        public int RegisterDeals(IEnumerable<TeamLeaderDeal> deals)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Deals.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderDeal d in deals)
                {
                    maxId++;
                    Deal newDeal = new Deal
                    {
                        Id = maxId,
                        TeamLeaderId = d.Id.GetValueOrDefault(),
                        Title = d.Title,
                        CustomerName = d.CustomerName,
                        QuatationNr = d.QuotationNr,
                        TotalPriceExclVat = d.TotalPriceExclVat,
                        Probability = d.Probability,
                        PhaseId = d.PhaseId,
                        ResponsibleUserId = d.ResponsibleUserId,
                        EntryDate = d.EntryDate,
                        LatestActivityDate = d.LatestActivityDate,
                        CloseDate = d.CloseDate,
                        DateLost = d.DateLost,
                        ReasonRefused = d.ReasonRefused,
                        SourceId = d.SourceId,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Deals.Add(newDeal);
                }
                return dbCtx.SaveChanges();
            }  
        }

        public int RegisterCalls(IEnumerable<TeamLeaderCall> calls)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Calls.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderCall c in calls)
                {
                    maxId++;
                    Call newCall = new Call
                    {
                        Id = maxId,
                        TeamLeaderId = c.Id.GetValueOrDefault(),
                        DueDate = c.DueDate,
                        UserId = c.UserId,
                        UserName = c.UserName,
                        CallDateTime = c.CallDateTime,
                        Telephone = c.Telephone,
                        Gsm = c.Gsm,
                        RelatedProjectId = c.RelatedProjectId,
                        RelatedMilestoneId = c.RelatedMilestoneId,
                        ForType = c.ForType,
                        ForId = c.ForId,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Calls.Add(newCall);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterMeetings(IEnumerable<TeamLeaderMeeting> meetings)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Meetings.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderMeeting m in meetings)
                {
                    maxId++;
                    Meeting newMeeting = new Meeting
                    {
                        Id = maxId,
                        TeamLeaderId = m.Id.GetValueOrDefault(),
                        DurationMinutes = m.DurationMinutes,
                        DateTimeStamp = m.DateTimeStamp,
                        MeetingDate = m.MeetingDateTime,
                        RelatedProjectId = m.RelatedProjectId,
                        RelatedMilestoneId = m.RelatedMilestoneId,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Meetings.Add(newMeeting);
                }
                return dbCtx.SaveChanges();
            }           
        }

        public int RegisterContactCompanyRelations(IEnumerable<TeamLeaderContactCompanyRelation> contactCompanyRelations)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.ContactCompanies.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderContactCompanyRelation ccr in contactCompanyRelations)
                {
                    maxId++;
                    ContactCompany newContactCompany = new ContactCompany
                    {
                        Id = maxId,
                        TeamLeaderId = ccr.Id.GetValueOrDefault(),
                        ContactId = ccr.ContactId,
                        CompanyId = ccr.CompanyId,
                        Function = ccr.Function,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.ContactCompanies.Add(newContactCompany);
                }
                return dbCtx.SaveChanges();
            }
        }

        public int RegisterPlannedTasks(IEnumerable<TeamLeaderPlannedTask> plannedTasks)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.PlannedTasks.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderPlannedTask pt in plannedTasks)
                {
                    maxId++;
                    PlannedTask newPlannedTask = new PlannedTask
                    {
                        Id = maxId,
                        TodoId = pt.TodoId,
                        StartDate = pt.StartDate,
                        DurationMinutes = pt.DurationMinutes,
                        UserId = pt.UserId,
                        ProjectId = pt.ProjectId,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.PlannedTasks.Add(newPlannedTask);
                }
                return dbCtx.SaveChanges();
            }           
        }

        public int RegisterContactProjectRelations(int projectTeamLeaderId, IEnumerable<TeamLeaderContactProjectRelation> contactProjectRelations)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.ContactProjects.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderContactProjectRelation cpr in contactProjectRelations)
                {
                    maxId++;
                    ContactProject newContactProject = new ContactProject
                    {
                        Id = maxId,
                        ProjectTeamLeaderId = projectTeamLeaderId,
                        Group = cpr.Group,
                        ObjectType = cpr.ObjectType,
                        ObjectId = cpr.ObjectId,
                        Name = cpr.Name,
                        Role = cpr.Role,
                        Telephone = cpr.Telephone,
                        Email = cpr.Email,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.ContactProjects.Add(newContactProject);
                }
                return dbCtx.SaveChanges();
            }   
        }

        public int RegisterProducts(IEnumerable<TeamLeaderProduct> products)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.Products.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderProduct p in products)
                {
                    maxId++;
                    Product newProduct = new Product
                    {
                        Id = maxId,
                        ExternalId = p.ExternalId,
                        Name = p.Name,
                        Vat = p.Vat,
                        BookingAccount = p.BookingAccount,
                        BookingAccountId = p.BookingAccountId,
                        Stock = p.Stock,
                        Price = p.Price,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.Products.Add(newProduct);
                }
                return dbCtx.SaveChanges();
            } 
        }

        public int RegisterDealSources(IEnumerable<TeamLeaderDealSource> dealSources)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.DealSources.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderDealSource ds in dealSources)
                {
                    maxId++;
                    DealSource newDealSource = new DealSource
                    {
                        Id = maxId,
                        TeamLeaderId = ds.Id.GetValueOrDefault(),
                        Name = ds.Name,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.DealSources.Add(newDealSource);
                }
                return dbCtx.SaveChanges();
            }  
        }

        public int RegisterDealPhases(IEnumerable<TeamLeaderDealPhase> dealPhases)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                int maxId = dbCtx.DealPhases.Max(c => (int?)c.Id) ?? 0;
                foreach (TeamLeaderDealPhase dp in dealPhases)
                {
                    maxId++;
                    DealPhase newDealPhase = new DealPhase
                    {
                        Id = maxId,
                        TeamLeaderId = dp.Id.GetValueOrDefault(),
                        Name = dp.Name,
                        LastSyncedUtc = DateTime.UtcNow
                    };
                    dbCtx.DealPhases.Add(newDealPhase);
                }
                return dbCtx.SaveChanges();
            }    
        }

        public int EnrichMeeting(int meetingId, TeamLeaderMeetingDetail meetingDetail)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                Meeting meeting = dbCtx.Meetings.FirstOrDefault(m => m.TeamLeaderId == meetingId);
                if (meeting != null)
                {
                    meeting.Title = meetingDetail.Title;
                    meeting.Afwezig = meetingDetail.Afwezig;
                    meeting.Bedrijf = meetingDetail.Bedrijf;
                    meeting.Klant = meetingDetail.Klant;
                    meeting.Intern = meetingDetail.Intern;
                    meeting.Sales = meetingDetail.Sales;
                    meeting.Location = meetingDetail.Location;

                    int maxIdUser = dbCtx.MeetingUsers.Max(u => (int?)u.Id) ?? 0;
                    foreach (TeamLeaderMeetingUser au in meetingDetail.AttendingUsers)
                    {
                        if (au.UserId.HasValue)
                        {
                            maxIdUser++;
                            meeting.MeetingUsers.Add(new MeetingUser { Id = maxIdUser, UserTeamLeaderId = au.UserId.Value, UserName = au.Name });
                        }
                    }

                    int maxIdContact = dbCtx.MeetingContacts.Max(c => (int?)c.Id) ?? 0;
                    foreach (TeamLeaderMeetingContact ac in meetingDetail.AttendingContacts)
                    {
                        if (ac.ContactId.HasValue)
                        {
                            maxIdContact++;
                            meeting.MeetingContacts.Add(new MeetingContact { Id = maxIdContact, ContactTeamLeaderId = ac.ContactId.Value, ContactName = ac.Name });
                        }
                    }
                    return dbCtx.SaveChanges();
                }
                return 0;
            }
        }

        public int EnrichCompany(int companyId, TeamLeaderCompanyDetail companyDetail)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                Company company = dbCtx.Companies.FirstOrDefault(m => m.TeamLeaderId == companyId);
                if (company != null)
                {
                    company.Sector = companyDetail.Sector;
                    company.AccountManagerId = companyDetail.AccountManagerId;

                    return dbCtx.SaveChanges();
                }
                return 0;
            }
        }

        public int EnrichTask(int taskId, TeamLeaderTaskDetail taskDetail)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                Task task = dbCtx.Tasks.FirstOrDefault(t => t.TeamLeaderId == taskId);
                if (task != null)
                {
                    task.DueDate = taskDetail.DueDate;
                    task.ResponsibleUserId = taskDetail.ResponsibleUserId;
                    return dbCtx.SaveChanges();
                }
                return 0;
            }
        }

        public int EnrichCall(int callId, TeamLeaderCallDetail callDetail)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                Call call = dbCtx.Calls.FirstOrDefault(c => c.TeamLeaderId == callId);
                if (call != null)
                {
                    call.Klant = callDetail.Klant;
                    call.Intern = callDetail.Intern;
                    call.Sales = callDetail.Sales;
                    return dbCtx.SaveChanges();
                }
                return 0;
            }
        }

        public IEnumerable<int> RetrieveUserTeamLeaderIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Users.Select(u => u.TeamLeaderId).ToArray();
            }
        }

        public IEnumerable<int> RetrieveProjectTeamLeaderIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Projects.Select(p => p.TeamLeaderId).ToArray();
            }
        }

        public IEnumerable<int> RetrieveDealIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Deals.Select(p => p.TeamLeaderId).ToArray();
            }
        }

        public IEnumerable<int> RetrieveMeetingIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Meetings.Select(p => p.TeamLeaderId).ToArray();
            }
        }

        public IEnumerable<int> RetrieveCompanyIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Companies.Select(c => c.TeamLeaderId).ToArray();
            }
        }

        public IEnumerable<int> RetrieveTaskIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Tasks.Select(t => t.TeamLeaderId).ToArray();
            }
        }

        public IEnumerable<int> RetrieveCallIds()
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.Calls.Select(c => c.TeamLeaderId).ToArray();
            }
        }

        public IDictionary<int, int?> RetrieveTimeTrackingRelatedTaskIds(int fromTimeTrackingId, int numberOfRecordsToRetrieve)
        {
            using (var dbCtx = new TeamLeaderEntities())
            {
                return dbCtx.TimeTrackings.Where(t => t.RelatedToObjectType == "task" && t.Id > fromTimeTrackingId).OrderBy(t => t.Id).Take(numberOfRecordsToRetrieve).ToDictionary(t => t.Id, t => t.RelatedToObjectId);
            }
        }
    }
}
