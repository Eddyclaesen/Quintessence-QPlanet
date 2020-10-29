using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Quintessence.QCandidate.Infrastructure.Dapper
{
    public class StoredProcedureCommandDefinition
	{
		private readonly string _storedProcedureName;

		private readonly IEnumerable<SqlParameter> _parameters;

		public StoredProcedureCommandDefinition(string storedProcedureName, params SqlParameter[] parameters)
		{
			_storedProcedureName = storedProcedureName;
			_parameters = parameters;
		}

		public CommandDefinition ToCommandDefinition()
		{
			var parameters = new DynamicParameters();
			foreach (var p in _parameters)
			{
				parameters.Add(p.ParameterName, p.Value, p.DbType);
			}

			return new CommandDefinition(_storedProcedureName, parameters, null, null, CommandType.StoredProcedure);
		}
	}
}