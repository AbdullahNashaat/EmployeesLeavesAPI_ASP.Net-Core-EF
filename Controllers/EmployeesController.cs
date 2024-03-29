﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesLeavesAPI.Contexts;
using EmployeesLeavesAPI.Models;
using EmployeesLeavesAPI.Repository;

namespace EmployeesLeavesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmloyeeLeavesContext _context;
        private readonly UnitOfWork _unitOfWork;
        public EmployeesController(EmloyeeLeavesContext context)
        {
        https://translate.google.com/
            _context = context;
            //_unitOfWork = unitOfWork;
            _unitOfWork = new UnitOfWork(_context);
        }

        //// GET: api/Employees
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        //{
        //    if (_context.Employees == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Employees.ToListAsync();
        //}

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _unitOfWork.Employees.GetAll();          
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    if (_context.Employees == null)
        //    {
        //        return Problem("Entity set 'EmloyeeLeavesContext.Employees'  is null.");
        //    }
        //    employee.EmployeeLeaves = new List<EmployeeLeave>
        //        {
        //            new EmployeeLeave { LeaveTypeId = 1, AmountOfDays = 7 },
        //            new EmployeeLeave { LeaveTypeId = 2, AmountOfDays = 14 }
        //        };
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        [HttpPost]
        public void PostEmployee(Employee employee)
        {
            employee.EmployeeLeaves = new List<EmployeeLeave>
                {
                    new EmployeeLeave { LeaveTypeId = 1, AmountOfDays = 7 },
                    new EmployeeLeave { LeaveTypeId = 2, AmountOfDays = 14 }
                };
            _unitOfWork.Employees.Add(employee);
            _unitOfWork.Complete();
        }
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            if (null != _context.EmployeesLeaves.FirstOrDefault(EL => EL.EmployeeId == employee.Id))
                return BadRequest("There are leave Balance of this Employee, you have to delete them first");

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
