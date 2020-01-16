using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.DataModel.Sec
{    
    public class AuthenticationToken
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public DateTime IssuedOn { get; set; }
    }
}
