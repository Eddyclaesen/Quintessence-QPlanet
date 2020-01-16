using System;
using System.ComponentModel.DataAnnotations;

namespace Quintessence.Infrastructure.Model.DataModel
{
    public interface IEntity : IValidatableObject
    {
        Guid Id { get; set; }
        Audit Audit { get; set; }
    }
}
