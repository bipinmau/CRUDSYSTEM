using DRDOAssignment.Data;
using DRDOAssignment.DTO;
using DRDOAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DRDOAssignment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region  GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomers()
        {
            return await _context.CustomerInfos
                .Include(c => c.Gender)
                .Include(c => c.District)
                    .ThenInclude(d => d.State)
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    GenderName = c.Gender.Name,
                    StateName = c.District.State.Name,
                    DistrictName = c.District.Name
                })
                .ToListAsync();
        }
        #endregion

        #region GET: api/Customer/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfo>> GetCustomer(int id)
        {
            var customer = await _context.CustomerInfos
                .Include(c => c.Gender)
                .Include(c => c.District)
                    .ThenInclude(d => d.State)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        #endregion

        #region POST: api/Customer/CreateCustomer
        [HttpPost]
        public async Task<ActionResult<CustomerInfosave>> CreateCustomer([FromBody] CustomerInfosave customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerInfos.Add(new CustomerInfo
            {
                Name = customer.Name,
                GenderId = customer.GenderId,
                DistrictId = customer.DistrictId
            });

            await _context.SaveChangesAsync();

            return Ok(customer);
        }
        #endregion

        #region PUT: api/Customer/UpdateCustomer/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerEditDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var customer = await _context.CustomerInfos.FindAsync(id);
            if (customer == null)
                return NotFound();

            customer.Name = dto.Name;
            customer.GenderId = dto.GenderId;
            customer.DistrictId = dto.DistrictId;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.CustomerInfos.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.CustomerInfos.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region GET: api/Customer/GetDistrictsByState/5
        [HttpGet("GetDistrictsByState/{stateId}")]
        public async Task<ActionResult<IEnumerable<DistrictViewModel>>> GetDistrictsByState(short stateId)
        {
            return await _context.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new DistrictViewModel
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToListAsync();
        }
        #endregion
        
    }
} 