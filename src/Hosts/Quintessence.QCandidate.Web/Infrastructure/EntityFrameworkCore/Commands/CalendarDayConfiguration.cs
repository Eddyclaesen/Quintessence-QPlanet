using Kenze.Infrastructure.EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands
{
    public class CalendarDayConfiguration : ChangeableGuidEntityMap<CalendarDay>
    {
        public override void Configure(EntityTypeBuilder<CalendarDay> builder)
        {
            base.Configure(builder);

            builder.ToTable("CalendarDays");
            builder.Property(entity => entity.MemoProgramComponentId).IsRequired();
            builder.Property(entity => entity.Day).IsRequired();
            builder.Property(entity => entity.Note).IsRequired();

        }
    }
}