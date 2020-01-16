using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Rep
{
    public class ReportParameter : EntityBase
    {
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public string DefaultText { get; set; }
    }
}