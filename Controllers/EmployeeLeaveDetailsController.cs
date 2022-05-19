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
    public class EmployeeLeaveDetailsController : ControllerBase
    {
        private readonly EmloyeeLeavesContext _context;

        public EmployeeLeaveDetailsController(EmloyeeLeavesContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeLeaveDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeLeaveDetail>>> GetEmployeeLeaveDetails()
        {
          if (_context.EmployeeLeaveDetails == null)
          {
              return NotFound();
          }
            return await _context.EmployeeLeaveDetails.ToListAsync();
        }

        // GET: api/EmployeeLeaveDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeLeaveDetail>> GetEmployeeLeaveDetail(int id)
        {
          if (_context.EmployeeLeaveDetails == null)
          {
              return NotFound();
          }
            var employeeLeaveDetail = await _context.EmployeeLeaveDetails.FindAsync(id);

            if (employeeLeaveDetail == null)
            {
                return NotFound();
            }

            return employeeLeaveDetail;
        }

        // PUT: api/EmployeeLeaveDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeLeaveDetail(int id, EmployeeLeaveDetail employeeLeaveDetail)
        {
            if (id != employeeLeaveDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeLeaveDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeLeaveDetailExists(id))
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

        // POST: api/EmployeeLeaveDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeLeaveDetail>> PostEmployeeLeaveDetail(EmployeeLeaveDetail employeeLeaveDetail)
        {
          if (_context.EmployeeLeaveDetails == null)
          {
              return Problem("Entity set 'EmloyeeLeavesContext.EmployeeLeaveDetails'  is null.");
          }
            var differenceDays = (employeeLeaveDetail.EndDate - employeeLeaveDetail.StartDate).TotalDays;
            var employeesLeave = _context.EmployeesLeaves.Find(employeeLeaveDetail.EmployeeLeaveId);

            if (differenceDays > employeesLeave.AmountOfDays)
            {
                return Problem("Not Enough Leave Balance");
            }
            employeeLeaveDetail.employeeLeave = employeesLeave;
            employeesLeave.AmountOfDays -= Convert.ToInt32(differenceDays);
            _context.Entry(employeesLeave).State = EntityState.Modified;
            _context.EmployeeLeaveDetails.Add(employeeLeaveDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeLeaveDetail", new { id = employeeLeaveDetail.Id }, employeeLeaveDetail);
        }

        // DELETE: api/EmployeeLeaveDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeLeaveDetail(int id)
        {
            if (_context.EmployeeLeaveDetails == null)
            {
                return NotFound();
            }
            var employeeLeaveDetail = await _context.EmployeeLeaveDetails.FindAsync(id);
            if (employeeLeaveDetail == null)
            {
                return NotFound();
            }

            _context.EmployeeLeaveDetails.Remove(employeeLeaveDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeLeaveDetailExists(int id)
        {
            return (_context.EmployeeLeaveDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
