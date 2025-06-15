using DRDOAssignment.Data;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DRDOAssignment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GET: api/State
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        {
            return await _context.States.ToListAsync();
        }
        #endregion

        #region GET: api/State/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(short id)
        {
            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }
        #endregion

        #region POST: api/State
        [HttpPost]
        public async Task<ActionResult<State>> CreateState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetState), new { id = state.Id }, state);
        }
        #endregion

        #region PUT: api/State/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState(short id, State state)
        {
            if (id != state.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region DELETE: api/State/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(short id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool StateExists(short id)
        {
            return _context.States.Any(e => e.Id == id);
        }
    }
} 