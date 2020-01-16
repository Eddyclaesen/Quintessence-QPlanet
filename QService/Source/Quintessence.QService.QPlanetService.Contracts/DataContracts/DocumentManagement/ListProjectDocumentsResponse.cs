using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QueryModel.Dom;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DocumentManagement
{
    [DataContract]
    public class ListProjectDocumentsResponse : DocumentManagementResponseBase
    {
        private List<DocumentView> _documents;

        [DataMember]
        public List<DocumentView> Documents
        {
            get { return _documents ?? (_documents = new List<DocumentView>()); }
            set { _documents = value; }
        }

        [DataMember]
        public DataTablePaging DataTablePaging { get; set; }
    }
}
