using System;
using DapperTest.Models;

namespace DapperTest.Repositories
{
	public interface INycBlockRepository
	{
		Task<IEnumerable<NycBlock>> GetAll();

        Task<NycBlock?> GetItem(int id);

        Task Add(NycBlock block);

        Task Update(int id, NycBlock block);

        Task Delete(int id);


        Task<string> GetVersion();
    }
}

