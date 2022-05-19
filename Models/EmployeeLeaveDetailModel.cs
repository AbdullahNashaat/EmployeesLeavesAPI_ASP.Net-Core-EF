namespace EmployeesLeavesAPI.Models
{
    public class EmployeeLeaveDetail
    {
        public int Id { get; set; }
        public int EmployeeLeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual EmployeeLeave? employeeLeave { get; set; }

    }
}
