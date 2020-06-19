using System.Data.Common;

namespace Quintessence.QCandidate.Infrastructure.Interfaces
{
    public interface IDbConnectionFactory
    {
        DbConnection Create();
    }
}
