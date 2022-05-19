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
    public class EmployeeLeavesController : ControllerBase
    {
        private readonly EmloyeeLeavesContext _context;

        public EmployeeLeavesController(EmloyeeLeavesContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeLeaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeLeave>>> GetEmployeesLeaves()
        {
            if (_context.EmployeesLeaves == null)
            {
                return NotFound();
            }
            return await _context.EmployeesLeaves.ToListAsync();
        }

        // GET: api/EmployeeLeaves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeLeave>> GetEmployeeLeave(int id)
        {
            if (_context.EmployeesLeaves == null)
            {
                return NotFound();
            }
            var employeeLeave = await _context.EmployeesLeaves.FindAsync(id);

            if (employeeLeave == null)
            {
                return NotFound();
            }

            return employeeLeave;
        }

        // PUT: api/EmployeeLeaves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeLeave(int id, EmployeeLeave employeeLeave)
        {
            if (id != employeeLeave.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeLeave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeLeaveExists(id))
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

        [HttpPut("changeammount")]
        public async Task<IActionResult> PutAmountOfDaysByEmployeeId( EmployeeLeave employeeLeave)
        {
            int Eid = employeeLeave.EmployeeId;
            int Tid = employeeLeave.LeaveTypeId;
            // null != _context.EmployeesLeaves.FirstOrDefault(EL => EL.LeaveTypeId == leaveType.Id)
            //if (null == _context.EmployeesLeaves.FirstOrDefault(EL => EL.LeaveTypeId == employeeLeave.LeaveTypeId))                
            //    return BadRequest("this Leave Type is not found ");

            //if (null == _context.EmployeesLeaves.FirstOrDefault(EL => EL.EmployeeId == employeeLeave.EmployeeId))               
            //     return BadRequest("this Employee is not found ");

            // if (null == _context.EmployeesLeaves.FirstOrDefault(EL => (EL.EmployeeId == employeeLeave.EmployeeId)&& (EL.LeaveTypeId == employeeLeave.LeaveTypeId)))
            //    return BadRequest("this Employee with this Leave Type is not found ");
            EmployeeLeave employeeLeave_EidLTid = _context.EmployeesLeaves.FirstOrDefault(EL =>
            (EL.EmployeeId == employeeLeave.EmployeeId) &&
            (EL.LeaveTypeId == employeeLeave.LeaveTypeId)
            );
            employeeLeave_EidLTid.AmountOfDays = employeeLeave.AmountOfDays;
            _context.Entry(employeeLeave_EidLTid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeLeaveExists_EidLTid(Eid, Tid))
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







        // POST: api/EmployeeLeaves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeLeave>> PostEmployeeLeave(EmployeeLeave employeeLeave)
        {
            if (_context.EmployeesLeaves == null)
            {
                return Problem("Entity set 'EmloyeeLeavesContext.EmployeesLeaves'  is null.");
            }
            //if (_context.Employees.Find(employeeLeave.EmployeeId).gender == 0)
            //{
            //    if (employeeLeave.LeaveTypeId == 4)
            //        return Problem("A male employee can't get a perantal leave");
            //}
            //else
            //{
            //    if (employeeLeave.LeaveTypeId == 3)
            //        return Problem("A female employee can't get a military leave");
            //}
         //   if(checkLeaveLimit)

            employeeLeave.employee = _context.Employees.Find(employeeLeave.EmployeeId);
            employeeLeave.leaveType = _context.LeaveTypes.Find(employeeLeave.LeaveTypeId);
            _context.EmployeesLeaves.Add(employeeLeave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeLeave", new { id = employeeLeave.Id }, employeeLeave);
        }

        // DELETE: api/EmployeeLeaves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeLeave(int id)
        {

            if (_context.EmployeesLeaves == null)
            {
                return NotFound();
            }
            var employeeLeave = await _context.EmployeesLeaves.FindAsync(id);
            if (employeeLeave == null)
            {
                return NotFound();
            }

            if (null != _context.EmployeeLeaveDetails.FirstOrDefault(EL => EL.EmployeeLeaveId == employeeLeave.Id))
                return BadRequest("There are Employee leaves details of this leave , you have to delete them first");

            _context.EmployeesLeaves.Remove(employeeLeave);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeLeaveExists(int id)
        {
            return (_context.EmployeesLeaves?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool EmployeeLeaveExists_EidLTid(int Eid,int LTid)
        {
            return (_context.EmployeesLeaves?.Any(e =>( e.EmployeeId == Eid)&&(e.LeaveTypeId == LTid))).GetValueOrDefault();
        }
    }
}
