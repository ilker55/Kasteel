using Microsoft.AspNetCore.Mvc;
using Kasteel.Models;
using Kasteel.DAL.Interfaces;

namespace Kasteel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastlesController(ICastleRepository repository) : ControllerBase
    {
        private readonly ICastleRepository _repository = repository;

        // GET: api/Castles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Castle>>> GetCastles()
        {
            return await _repository.GetAll();
        }

        // GET: api/Castles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Castle>> GetCastle(int id)
        {
            var castle = await _repository.GetByID(id);
            return castle == null
                ? NotFound()
                : castle;
        }

        // PUT: api/Castles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCastle(int id, Castle castle)
        {
            if (id != castle.Id)
                return BadRequest();

            _repository.Update(castle);
            await _repository.Save();

            return NoContent();
        }

        // POST: api/Castles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Castle>> PostCastle(Castle castle)
        {
            await _repository.Insert(castle);
            await _repository.Save();

            return CreatedAtAction("GetCastle", new { id = castle.Id }, castle);
        }

        // DELETE: api/Castles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCastle(int id)
        {
            if (!await _repository.Delete(id))
                return NotFound();

            await _repository.Save();

            return NoContent();
        }
    }
}
