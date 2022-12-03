using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperTest.Models;
using DapperTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Npgsql;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NycBlockController : ControllerBase
    {
        private readonly INycBlockRepository _repository;

        public NycBlockController(INycBlockRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("version")]
        public async Task<string> GetVersion()
        {
            return await _repository.GetVersion();
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var blocks = await _repository.GetAll();
            if (blocks is null)
                return NotFound();

            return Ok(blocks);
        }

        [HttpGet("{id}", Name ="BlockById")]
        public async Task<IActionResult> GetItem(int id)
        {
            var block = await _repository.GetItem(id);
            if (block is null)
                return NotFound();

            return Ok(block);
        }
    }
}

