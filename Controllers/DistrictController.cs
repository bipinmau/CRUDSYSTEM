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
    public class DistrictController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistrictController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GET: api/District
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictViewModel>>> GetDistricts()
        {
            var districts = await _context.Districts
                .Include(d => d.State)
                .Select(d => new DistrictViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    StateId = d.StateId,
                    StateName = d.State.Name
                })
                .ToListAsync();

            return Ok(districts);
        }
        #endregion

        #region GET: api/District/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictViewModel>> GetDistrict(short id)
        {
            var district = await _context.Districts
                .Include(d => d.State)
                .Where(d => d.Id == id)
                .Select(d => new DistrictViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    StateId = d.StateId,
                    StateName = d.State.Name
                })
                .FirstOrDefaultAsync();

            if (district == null)
                return NotFound();

            return Ok(district);
        }
        #endregion

        #region GET: api/District/GetByState/5
        [HttpGet("GetByState/{stateId}")]
        public async Task<ActionResult<IEnumerable<DistrictViewModel>>> GetDistrictsByState(short stateId)
        {
            var districts = await _context.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new DistrictViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    StateId = d.StateId,
                    StateName = d.State.Name
                })
                .ToListAsync();

            return Ok(districts);
        }
        #endregion

        #region POST: api/District
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DistrictInputModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var district = new District
            {
                Name = model.Name,
                StateId = model.StateId
            };

            _context.Districts.Add(district);
            await _context.SaveChangesAsync();

            return Ok(new { message = "District added successfully" });
        }
        #endregion

        #region PUT: api/District/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, [FromBody] DistrictInputModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var district = await _context.Districts.FindAsync(id);
            if (district == null)
                return NotFound();

            district.Name = model.Name;
            district.StateId = model.StateId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(new { message = "District updated successfully" });
        }
        #endregion

        #region DELETE: api/District/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
                return NotFound();

            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();

            return Ok(new { message = "District deleted successfully" });
        }
        #endregion

        private bool DistrictExists(short id)
        {
            return _context.Districts.Any(d => d.Id == id);
        }
    }
}
