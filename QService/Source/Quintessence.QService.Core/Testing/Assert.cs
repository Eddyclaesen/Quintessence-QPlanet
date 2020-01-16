using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quintessence.QService.Core.Testing
{
    public static class ExtendedAssert
    {
        public static void IsLargerThan<TValue>(TValue expected, TValue value)
            where TValue : IComparable
        {
            if (value.CompareTo(expected) <= 0)
                throw new AssertFailedException(string.Format("Value '{0}' is smaller than or equal to '{1}'", value, expected));
        }
    }
}
