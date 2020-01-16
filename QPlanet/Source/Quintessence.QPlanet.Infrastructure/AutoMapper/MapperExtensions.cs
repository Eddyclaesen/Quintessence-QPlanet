using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Quintessence.QPlanet.Infrastructure.AutoMapper
{
    public static class MapperExtensions
    {
        public static IEnumerable<TDestination> MapList<TDestination>(IEnumerable<object> items)
        {
            return items.Select(Mapper.Map<TDestination>);
        }
    }
}
