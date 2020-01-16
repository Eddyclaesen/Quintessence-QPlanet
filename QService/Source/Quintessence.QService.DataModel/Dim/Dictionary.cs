using System.Runtime.Serialization;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Dim
{
    public class Dictionary : EntityBase
    {
        private string _name;
        private string _description;
        public int? ContactId { get; set; }
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description ?? string.Empty; }
            set { _description = value; }
        }

        public bool Current { get; set; }
        public int LegacyId { get; set; }
        public bool IsLive { get; set; }
    }
}
