using System;
using System.ComponentModel.DataAnnotations;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Sec
{
    public class User : EntityBase
    {
        public int AssociateId { get; set; }
        public Guid? RoleId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool ChangePassword { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsLocked { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}
