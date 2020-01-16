using System;

namespace Quintessence.QPlanet.ViewModel.Sec
{
    public class AuthenticationTokenModel
    {
        public Guid Id { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public DateTime IssuedOn { get; set; }
    }
}