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
    public class GenderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenderController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GET: api/Gender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            return await _context.Genders.ToListAsync();
        }
        #endregion

        #region GET: api/Gender/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gender>> GetGender(byte id)
        {
            var gender = await _context.Genders.FindAsync(id);

            if (gender == null)
            {
                return NotFound();
            }

            return gender;
        }
        #endregion

        #region POST: api/Gender
        [HttpPost]
        public async Task<ActionResult<Gender>> CreateGender(Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genders.Add(gender);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGender), new { id = gender.Id }, gender);
        }
        #endregion

        #region PUT: api/Gender/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGender(byte id, Gender gender)
        {
            if (id != gender.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(gender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        #region DELETE: api/Gender/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(byte id)
        {
            var gender = await _context.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            _context.Genders.Remove(gender);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        private bool GenderExists(byte id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
} 