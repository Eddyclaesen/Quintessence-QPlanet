using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Wsm
{
    public class UserProfile : EntityBase
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
}