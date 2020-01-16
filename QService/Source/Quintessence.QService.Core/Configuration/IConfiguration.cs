namespace Quintessence.QService.Core.Configuration
{
    public interface IConfiguration
    {
        string GetConnectionStringConfiguration<TContext>();
    }
}
