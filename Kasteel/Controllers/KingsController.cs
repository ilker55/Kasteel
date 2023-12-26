using Microsoft.AspNetCore.Mvc;
using Kasteel.DAL;
using Kasteel.Models;

namespace Kasteel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KingsController(IKingRepository repository) : ControllerBase
    {
        private readonly IKingRepository _repository = repository;

        // GET: api/Kings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<King>>> GetKings()
            => await _repository.GetAll();

        // GET: api/Kings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<King>> GetKing(int id)
        {
            var king = await _repository.GetByID(id);
            return king == null
                ? NotFound()
                : king;
        }

        // PUT: api/Kings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKing(int id, King king)
        {
            if (id != king.Id)
                return BadRequest();

            _repository.Update(king);

            try
            {
                await _repository.Save();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Kings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<King>> PostKing(King king)
        {
            await _repository.Insert(king);
            await _repository.Save();

            return CreatedAtAction("GetKing", new { id = king.Id }, king);
        }

        // DELETE: api/Kings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKing(int id)
        {
            var deleted = await _repository.Delete(id);
            if (!deleted)
                return NotFound();

            await _repository.Save();

            return NoContent();
        }
    }
}
