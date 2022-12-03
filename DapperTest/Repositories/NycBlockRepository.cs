using System;
using System.Data;
using Dapper;
using DapperTest.Models;

namespace DapperTest.Repositories
{
	public class NycBlockRepository: INycBlockRepository
	{
        //private readonly PgContext _context;

        private readonly IDbConnection _connection;

        private const string _tableName = "nyc_census_blocks";

        public NycBlockRepository(PgContext context)
		{
			//_context = context;
            _connection = context.CreateConnection();
		}

        public Task Add(NycBlock block)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<NycBlock?> GetItem(int id)
        {
            var sql = @$"SELECT blkid,fid AS id,popn_total,popn_white,popn_black,boroname ,geom FROM {_tableName}
                         Where fid = @id";

            //using var connection = _context.CreateConnection();

            try
            {
                var block = await _connection.QuerySingleAsync<NycBlock>(sql, new { id });
                return block;
            }
            catch
            {
                return null;
            }

            
        }

        public async Task<IEnumerable<NycBlock>> GetAll()
        {
            var sql = $"SELECT blkid,fid AS id,popn_total,popn_white,popn_black,boroname ,geom FROM {_tableName} LIMIT 30";

            //using var connection = _context.CreateConnection();

            var blocks = await _connection.QueryAsync<NycBlock>(sql);
            return blocks;
        }

        

        public Task Update(int id, NycBlock block)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetVersion()
        {
            var sql = "SELECT version()";

            //using var connection = _context.CreateConnection();
            var value = await _connection.ExecuteScalarAsync<string>(sql);
            return value;
        }
    }
}

