using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using DapperTest.Dtos;
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


        /// <summary>
        /// Create a model and insert into database.
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<NycBlock> Add(CreateNycBlockDto block)
        {
            var sql = $@"INSERT INTO {_tableName} (fid,blkid,popn_total,popn_white,popn_black,boroname )
                         VALUES (@fid,@blkid,@popn_total,@popn_white,@popn_black,@boroname )";

            var parameters = new DynamicParameters();
            parameters.Add("fid", block.Id, DbType.Int32);
            parameters.Add("blkid", block.Blkid, DbType.String);
            parameters.Add("boroname", block.Boroname, DbType.String);
            parameters.Add("popn_total", block.Popn_total, DbType.Int32);
            parameters.Add("popn_white", block.Popn_white, DbType.Int32);
            parameters.Add("popn_black", block.Popn_black, DbType.Int32);

            await _connection.ExecuteAsync(sql, parameters);

            NycBlock nycBlock = new NycBlock
            {
                Id = block.Id,// this id should be auto generated instead of hard coding.
                Blkid = block.Blkid,
                Boroname = block.Boroname,
                Popn_total = block.Popn_total,
                Popn_white = block.Popn_white,
                Popn_black = block.Popn_black
            };

            return nycBlock;
        }

        public async Task Update(int id, UpdateNycBlockDto block)
        {
            var sql = @$"UPDATE {_tableName}
                         SET blkid = @blkid,popn_total = @popn_total,popn_white = @popn_white,popn_black = @popn_black,boroname = @boroname
                         WHERE fid = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("blkid", block.Blkid, DbType.String);
            parameters.Add("boroname", block.Boroname, DbType.String);
            parameters.Add("popn_total", block.Popn_total, DbType.Int32);
            parameters.Add("popn_white", block.Popn_white, DbType.Int32);
            parameters.Add("popn_black", block.Popn_black, DbType.Int32);

            await _connection.ExecuteAsync(sql, parameters);
        }

        

        public async Task<NycBlock?> GetItem(int id)
        {   
            var sql = @$"SELECT blkid,fid AS id,popn_total,popn_white,popn_black,boroname ,geom FROM {_tableName}
                         Where fid = @id";

            var block = await _connection.QuerySingleOrDefaultAsync<NycBlock>(sql, new { id }); //for class, default is null
            return block; 
        }

        public async Task<IEnumerable<NycBlock>> GetAll()
        {
            var sql = $"SELECT blkid,fid AS id,popn_total,popn_white,popn_black,boroname ,geom FROM {_tableName} LIMIT 30";

            var blocks = await _connection.QueryAsync<NycBlock>(sql);
            return blocks;
        }


        public async Task Delete(int id)
        {
            var sql = $@"DELETE FROM {_tableName} WHERE fid = @id";

            await _connection.ExecuteAsync(sql, new { id });
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

