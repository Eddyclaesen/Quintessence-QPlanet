using System;

namespace Quintessence.CulturalFit.Infra.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }

        Audit Audit { get; set; }
    }
}
