using Microsoft.AspNetCore.Mvc;
using Snippr.Application.Data;
using Snippr.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Snippr.API.Controllers
{
    public class SnippetsController : BaseController
    {
        private readonly IRepository _repository;

        public SnippetsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> PostSnippet([FromBody] Snippet snippet)
        {
            await _repository.WriteSnippet(snippet);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSnippet([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _repository.GetSnippet(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSnippet([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _repository.DeleteSnippet(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSnippet([FromQuery] Guid id, [FromBody] Snippet snippet)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _repository.UpdateSnippet(id, snippet);
            return Ok();
        }
    }
}
