using System;
using System.Data;
using Npgsql;

namespace DapperTest.Repositories
{
	public class PgContext
	{
        private readonly IConfiguration _configuration;

		private readonly string _connectionString;

        public PgContext(IConfiguration configuration)
		{
			_configuration = configuration;
            var connectionStr = _configuration.GetConnectionString("PostgreSQL");

			ArgumentNullException.ThrowIfNull(connectionStr, "Please provide a valid connection string.");

			_connectionString = connectionStr;
		}


		public IDbConnection CreateConnection()
		{
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);
            dataSourceBuilder.UseNetTopologySuite();
            var dataSource = dataSourceBuilder.Build();

            return dataSource.CreateConnection(); //TODO: find out he best way to create connection and life time of this connection
        }
	}
}

