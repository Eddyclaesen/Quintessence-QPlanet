using System;

namespace Quintessence.QPlanet.Infrastructure.Tools
{
    public static class Randomizer
    {
        private static readonly Random Instance = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return Instance.Next(minValue, maxValue);
        }
    }
}
