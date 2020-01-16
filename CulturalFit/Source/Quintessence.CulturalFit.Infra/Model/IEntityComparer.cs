namespace Quintessence.CulturalFit.Infra.Model
{
    public interface IEntityComparer<in TEntity>
        where TEntity : IEntity
    {
        bool Compare(TEntity entity);
    }
}
