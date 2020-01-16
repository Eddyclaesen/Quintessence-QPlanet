using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QCore.Logging;

namespace Quintessence.Core.Tests.Logging
{
    [TestClass]
    public class DurationLogTest
    {
        /// <summary>
        /// Tests the duration log
        /// </summary>
        [TestMethod]
        public void TestDurationLog()
        {
            DurationLog durationLog;

            using (durationLog = new DurationLog())
                Thread.Sleep(TimeSpan.FromMilliseconds(200));

            Assert.IsNotNull(durationLog);
            Assert.IsTrue(durationLog.Elapsed > TimeSpan.FromMilliseconds(150));
        }
    }
}
