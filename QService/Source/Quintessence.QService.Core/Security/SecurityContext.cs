using System;

namespace Quintessence.QService.Core.Security
{
    public class SecurityContext
    {
        public Guid TokenId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
