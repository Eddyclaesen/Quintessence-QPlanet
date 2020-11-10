using Kenze.Infrastructure.EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands
{
    public class MemoProgramComponentConfiguration : ChangeableGuidEntityMap<MemoProgramComponent>
    {
        public override void Configure(EntityTypeBuilder<MemoProgramComponent> builder)
        {
            base.Configure(builder);

            builder.ToTable("MemoProgramComponents");
            
            builder.Property(entity => entity.SimulationCombinationId)
                .IsRequired();

            builder.Property(entity => entity.UserId).IsRequired();

            builder.HasMany(entity => entity.Memos);

            builder.HasMany(entity => entity.CalendarDays);


        }
    }
}