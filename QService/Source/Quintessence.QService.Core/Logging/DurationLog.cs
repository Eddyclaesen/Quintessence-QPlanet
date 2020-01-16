using System;
using System.Diagnostics;

namespace Quintessence.QService.Core.Logging
{
    /// <summary>
    /// Disposable class to time the execution duration of the code in the body
    /// </summary>
    public class DurationLog : IDisposable
    {
        private Stopwatch _stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationLog"/> class.
        /// </summary>
        public DurationLog()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DurationLog" /> class.
        /// </summary>
        ~DurationLog()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _stopwatch.Stop();
                var trace = new StackTrace();

                //Get calling method
                var frame = trace.GetFrame(2);

                var method = frame.GetMethod();

                if (method.DeclaringType != null)
                    LogManager.LogDuration(string.Format("{0};{1};{2}", method.DeclaringType.FullName, method.Name, _stopwatch.ElapsedMilliseconds));

                _stopwatch = null;
            }
            // free native resources if there are any.
        }

        /// <summary>
        /// Creates the specified description.
        /// </summary>
        /// <returns></returns>
        public static DurationLog Create()
        {
            return new DurationLog();
        }
    }
}
