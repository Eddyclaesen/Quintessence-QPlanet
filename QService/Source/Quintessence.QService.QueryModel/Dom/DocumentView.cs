using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dom
{
    [DataContract]
    [Table("{9AFD4F74-F8F8-4D6F-AB31-45C0A44D1ADF}")]
    public class DocumentView : SharePointEntityView
    {
        [DataMember]
        [Column("FileRef")]
        public string Link { get; set; }

        [DataMember]
        [Column("FileLeafRef")]
        public string Name { get; set; }

        [DataMember]
        [Column("Contact_x003a__x0020_contact_id")]
        public int? ContactId { get; set; }

        [DataMember]
        [Column("Project_x003a__x0020_project_id")]
        public int? CrmProjectId { get; set; }

        [DataMember]
        public bool IsImportant { get; set; }
    }
}
