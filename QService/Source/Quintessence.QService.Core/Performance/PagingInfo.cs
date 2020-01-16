using System;
using System.Linq;
using System.Linq.Expressions;

namespace Quintessence.QService.Core.Performance
{
    public class PagingInfo
    {
        public string FilterTerm { get; set; }

        public int Page { get; set; }

        public int PageLength { get; set; }

        public int TotalRecords { get; set; }

        public int TotalDisplayRecords { get; set; }

        public bool HasMatch(params string[] values)
        {
            if (string.IsNullOrWhiteSpace(FilterTerm))
                return true;

            return FilterTerm
                .ToLowerInvariant()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .All(term => values.Any(value => !string.IsNullOrWhiteSpace(value) && value.ToLowerInvariant().Contains(term)));
        }
    }
}
