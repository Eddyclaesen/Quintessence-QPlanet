using Quintessence.QService.Core.Configuration;
using Quintessence.QService.Data.Interfaces.CommandContext;
using Quintessence.QService.DataModel.Dom;
using Quintessence.QService.SharePointData.Base;

namespace Quintessence.QService.SharePointData.CommandContext
{
    public class SharePointCommandContext : ClientContextBase<IDomCommandContext>, IDomCommandContext
    {
        public SharePointCommandContext(IConfiguration configuration)
            : base(configuration)
        {
        }

        public int PrepareTrainingChecklist()
        {
            var id = CreateItem<TrainingChecklist>();

            return id;
        }
    }
}
