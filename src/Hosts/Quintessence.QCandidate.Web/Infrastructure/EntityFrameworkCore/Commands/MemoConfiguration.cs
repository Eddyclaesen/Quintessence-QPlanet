using Kenze.Infrastructure.EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quintessence.QCandidate.Core.Domain;

namespace Quintessence.QCandidate.Infrastructure.EntityFrameworkCore.Commands
{
    public class MemoConfiguration : ChangeableGuidEntityMap<Memo>
    {
        public override void Configure(EntityTypeBuilder<Memo> builder)
        {
            base.Configure(builder);

            builder.ToTable("Memos");
            builder.Property(entity => entity.OriginId).IsRequired();
            builder.Property(entity => entity.MemoProgramComponentId).IsRequired();
            builder.Property(entity => entity.Position).IsRequired();


        }
    }
}
