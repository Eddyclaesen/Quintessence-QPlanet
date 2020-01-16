using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dom
{
    [DataContract]
    [Table("{F92204B5-675E-4BCB-8126-90660E5342DA}", Schema = "/ITWeb")]
    public class HelpEntryView : SharePointEntityView
    {
        [DataMember]
        [Column("Body")]
        public string Body { get; set; }

        [DataMember]
        [Column("Title")]
        public string Title { get; set; }
    }
}