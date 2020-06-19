using System.Data.Common;
using System.Data.SqlClient;
using Quintessence.QCandidate.Infrastructure.Interfaces;

namespace Quintessence.QCandidate.Infrastructure
{
	public class SqlDbConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connectionString;
		private readonly DbConnection _dbConnection;

		public SqlDbConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public SqlDbConnectionFactory(DbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}
		public DbConnection Create()
		{
			if (_dbConnection == null)
			{
				return new SqlConnection(_connectionString);
			}
			return _dbConnection;
		}
	}
}
