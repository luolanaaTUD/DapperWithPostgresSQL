using System;
using DapperTest.Models;
using DapperTest.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace DapperTest.Repositories
{
	public interface INycBlockRepository
	{
		Task<IEnumerable<NycBlock>> GetAll();

        Task<NycBlock?> GetItem(int id);

        Task<NycBlock> Add(CreateNycBlockDto block);

        Task Update(int id, UpdateNycBlockDto block);

        Task Delete(int id);


        Task<string> GetVersion();
    }
}

