using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Cam
{
    public class Candidate : EntityBase
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public int LanguageId { get; set; }

        public string Phone { get; set; }

        public string Reference { get; set; }
    }
}
