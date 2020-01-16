using System;
using System.Collections.Generic;

namespace Quintessence.CulturalFit.Infra
{
    public static class Extensions
    {
        public static void ForEach<TItem>(this IEnumerable<TItem> items, Action<TItem> action)
        {
            foreach (var item in items)
                action(item);
        }
    }
}
