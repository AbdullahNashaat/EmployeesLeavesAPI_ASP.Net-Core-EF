using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesLeavesAPI.Models
{
    
   
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnnualLimit { get; set; }
        public virtual ICollection<EmployeeLeave>? EmployeeLeaves { get; set; }


    }
    public enum Gender
    {
        Male,
        Female
    }
}
