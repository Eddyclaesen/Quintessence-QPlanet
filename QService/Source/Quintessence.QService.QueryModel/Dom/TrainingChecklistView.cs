using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dom
{
    [DataContract]
    [Table("{E461EE22-F3E0-44D7-AA1D-B9D08553729A}", Schema = "/ITWeb/QPlanet")]
    public class TrainingChecklistView : SharePointEntityView
    {
        [DataMember]
        public string EditLink { get; set; }
    }
}