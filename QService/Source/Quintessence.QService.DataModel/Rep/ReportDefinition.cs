using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Rep
{
    public class ReportDefinition : EntityBase
    {
        public int ReportTypeId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool ExportAsXml { get; set; }
        public bool ExportAsCsvl { get; set; }
        public bool ExportAsImg { get; set; }
        public bool ExportAsPdf { get; set; }
        public bool ExportAsMhtml { get; set; }
        public bool ExportAsHtml4 { get; set; }
        public bool ExportAsHtml32 { get; set; }
        public bool ExportAsExcel { get; set; }
        public bool ExportAsWord { get; set; }	
        public bool IsActive { get; set; }
    }
}