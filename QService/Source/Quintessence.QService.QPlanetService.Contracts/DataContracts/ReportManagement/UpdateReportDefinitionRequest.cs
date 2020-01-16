using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class UpdateReportDefinitionRequest : UpdateRequestBase
    {
        [DataMember]
        public int ReportTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public bool ExportAsXml { get; set; }

        [DataMember]
        public bool ExportAsCsvl { get; set; }

        [DataMember]
        public bool ExportAsImg { get; set; }

        [DataMember]
        public bool ExportAsPdf { get; set; }

        [DataMember]
        public bool ExportAsMhtml { get; set; }

        [DataMember]
        public bool ExportAsHtml4 { get; set; }

        [DataMember]
        public bool ExportAsHtml32 { get; set; }

        [DataMember]
        public bool ExportAsExcel { get; set; }

        [DataMember]
        public bool ExportAsWord { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}