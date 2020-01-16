using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.QService.Core.Performance;

namespace Quintessence.QService.Core.UnitTests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void TestAddWorkdays()
        {
            var date = DateTime.Now;
            
            //Test for friday
            while (date.DayOfWeek != DayOfWeek.Friday)
            {
                date = date.AddDays(1);
            }
            Assert.AreEqual(DayOfWeek.Friday, date.DayOfWeek);

            Assert.AreEqual(date.AddDays(4), date.AddWorkdays(2));

            //Test for saterday
            while (date.DayOfWeek != DayOfWeek.Saturday)
            {
                date = date.AddDays(1);
            }
            Assert.AreEqual(DayOfWeek.Saturday, date.DayOfWeek);

            Assert.AreEqual(date.AddDays(3), date.AddWorkdays(2));

            //Test for sunday
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            Assert.AreEqual(DayOfWeek.Sunday, date.DayOfWeek);

            Assert.AreEqual(date.AddDays(2), date.AddWorkdays(2));

            //Test for regular workday, e.g. monday
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(1);
            }
            Assert.AreEqual(DayOfWeek.Monday, date.DayOfWeek);

            Assert.AreEqual(date.AddDays(2), date.AddWorkdays(2));
        }

        [TestMethod]
        public void TestDifferenceInWorkdays()
        {
            var date = DateTime.Now;
            var compareDate = DateTime.Now.AddDays(7);

            Assert.AreEqual(5, date.DifferenceInWorkdays(compareDate));
            Assert.AreEqual(5, compareDate.DifferenceInWorkdays(date));

            //Try other day
            date = DateTime.Now.AddDays(3);
            compareDate = DateTime.Now.AddDays(10);

            Assert.AreEqual(5, date.DifferenceInWorkdays(compareDate));
            Assert.AreEqual(5, compareDate.DifferenceInWorkdays(date));

        }

        [TestMethod]
        public void TestSetTime()
        {
            var date = DateTime.Now;

            const int hours = 10;
            const int minutes = 0;
            const int seconds = 0;

            var newDate = date.SetTime(hours, minutes, seconds);

            Assert.AreEqual(date.Date, newDate.Date);
            Assert.AreEqual(new TimeSpan(hours, minutes, seconds), newDate.TimeOfDay);
        }
    }
}
