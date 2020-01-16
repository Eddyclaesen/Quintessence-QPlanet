using System;
using System.Diagnostics;

namespace Quintessence.QPlanet.Infrastructure.Logging
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
        /// Disposes the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="component">The component.</param>
        public void Dispose(string module, string component)
        {
            Dispose(true, module, component);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        /// <param name="module">The module.</param>
        /// <param name="component">The component.</param>
        protected virtual void Dispose(bool disposing, string module = null, string component = null)
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
                    LogManager.LogDuration(string.Format("{0};{1};{2}", module ?? method.DeclaringType.FullName, component ?? method.Name, _stopwatch.ElapsedMilliseconds));

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
