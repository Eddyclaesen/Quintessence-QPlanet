using AutoMapper;
using Quintessence.QPlanet.Webshell.Models.Shared;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QPlanet.Webshell.Infrastructure.TypeConverters
{
    public class DataTableParameterModelConverter : TypeConverter<DataTableParameterModel, DataTablePaging>
    {
        protected override DataTablePaging ConvertCore(DataTableParameterModel source)
        {
            var dataTablePaging = new DataTablePaging
            {
                FilterTerm = source.sSearch,
                Page = source.iDisplayStart,
                PageLength = source.iDisplayLength
            };
            return dataTablePaging;
        }
    }
}