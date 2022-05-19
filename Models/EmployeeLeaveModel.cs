using System.ComponentModel.DataAnnotations;

namespace EmployeesLeavesAPI.Models
{
    public class EmployeeLeave
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int AmountOfDays { get; set; }
        public virtual EmployeeModel? employee { get; set; }
        public virtual LeaveType? leaveType { get; set; }
    }
}
