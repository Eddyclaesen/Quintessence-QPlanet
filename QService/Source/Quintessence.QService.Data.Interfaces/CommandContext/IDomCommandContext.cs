using System;

namespace Quintessence.QService.Data.Interfaces.CommandContext
{
    public interface IDomCommandContext : IDisposable
    {
        int PrepareTrainingChecklist();
    }
}
