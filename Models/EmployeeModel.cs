namespace EmployeesLeavesAPI.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public Gender gender { get; set; }
        public virtual ICollection<EmployeeLeave>? EmployeeLeaves { get; set; }

    }
}
