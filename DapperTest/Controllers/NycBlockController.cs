using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperTest.Dtos;
using DapperTest.Models;
using DapperTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Npgsql;


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
        public async Task<IActionResult> GetAllBlocks()
        {
            var blocks = await _repository.GetAll();
            if (blocks is null)
                return NotFound();

            return Ok(blocks);
        }

        [HttpGet("{id}", Name ="BlockById")]
        public async Task<IActionResult> GetBlockByID(int id)
        {
            var block = await _repository.GetItem(id);
            if (block is null)
                return NotFound();

            return Ok(block);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlock([FromBody]CreateNycBlockDto blockDto)
        {
            var created = await _repository.Add(blockDto);

            return CreatedAtRoute("BlockById", new { id = created.Id }, created); // "Route name points to GetItem route.
            //return Ok(created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlock(int id, [FromBody] UpdateNycBlockDto blockDto)
        {
            var dbBlock = await _repository.GetItem(id);
            if (dbBlock is null)
                return NotFound();

            await _repository.Update(id, blockDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlock(int id)
        {
            var dbBlock = await _repository.GetItem(id);
            if (dbBlock is null)
                return NotFound();

            await _repository.Delete(id);
            return NoContent();
        }
    }
}

