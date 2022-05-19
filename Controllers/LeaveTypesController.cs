using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesLeavesAPI.Contexts;
using EmployeesLeavesAPI.Models;

namespace EmployeesLeavesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly EmloyeeLeavesContext _context;

        public LeaveTypesController(EmloyeeLeavesContext context)
        {
            _context = context;
        }

        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveType>>> GetLeaveTypes()
        {
          if (_context.LeaveTypes == null)
          {
              return NotFound();
          }
            return await _context.LeaveTypes.ToListAsync();
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int id)
        {
          if (_context.LeaveTypes == null)
          {
              return NotFound();
          }
            var leaveType = await _context.LeaveTypes.FindAsync(id);

            if (leaveType == null)
            {
                return NotFound();
            }

            return leaveType;
        }

        // PUT: api/LeaveTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveType(int id, LeaveType leaveType)
        {
            if (id != leaveType.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(id))
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

        // POST: api/LeaveTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveType(LeaveType leaveType)
        {
            if (_context.LeaveTypes == null)
            {
                return Problem("Entity set 'EmloyeeLeavesContext.LeaveTypes'  is null.");
            }
            _context.LeaveTypes.Add(leaveType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveType", new { id = leaveType.Id }, leaveType);
        }
        [HttpGet("setleavetype")]
        public async Task<ActionResult<IEnumerable<LeaveType>>> setLeaveType()
        {
           
            saveLeavesTypes("Casual", 7);          
            saveLeavesTypes("Schedual", 14);
            saveLeavesTypes("Military", 8);
            saveLeavesTypes("Parental", 30);

            return await _context.LeaveTypes.ToListAsync();
        }

        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveType(int id)
        {
            if (_context.LeaveTypes == null)
            {
                return NotFound();
            }
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            if (null != _context.EmployeesLeaves.FirstOrDefault(EL => EL.LeaveTypeId == leaveType.Id))
                return BadRequest("There are leave Balances of this Leave Type, you have to delete them first");
            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveTypeExists(int id)
        {
            return (_context.LeaveTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private void  saveLeavesTypes(string name,int anuualLimit)
        {
            if (null == _context.LeaveTypes.FirstOrDefault(te => te.Name == name))
            {
                LeaveType leaveType = new LeaveType
                {
                    Name = name,
                    AnnualLimit = anuualLimit
                };
                _context.LeaveTypes.Add(leaveType);
                _context.SaveChanges();
            }
           // return Ok();

        }
    }
}
