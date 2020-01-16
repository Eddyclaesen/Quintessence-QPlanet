using System;
using System.Diagnostics;

namespace Quintessence.CulturalFit.Infra.Logging
{
    /// <summary>
    /// Disposable class to time the execution duration of the code in the body
    /// </summary>
    public class DurationLog : IDisposable
    {
        private readonly string _description;
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationLog"/> class.
        /// </summary>
        public DurationLog(string description = null)
        {
            _description = description;
            _stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _stopwatch.Stop();
            var trace = new StackTrace();

            //Get calling method
            var frame = trace.GetFrame(1);

            var method = frame.GetMethod();

            LogManager.LogTrace(_description != null
                            ? string.Format("{0}.{1}: {2} - Duration: {3}ms", method.DeclaringType.FullName, method.Name, _description, _stopwatch.ElapsedMilliseconds)
                            : string.Format("{0}.{1} - Duration: {2}ms", method.DeclaringType.FullName, method.Name, _stopwatch.ElapsedMilliseconds));
        }

        /// <summary>
        /// Creates the specified description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static DurationLog Create(string description = null)
        {
            return new DurationLog(description);
        }
    }
}
