namespace EmployeesLeavesAPI.Repository.RepositoryInterfaces
{
    public interface IUnitOfWork
    {
        IEmployeeLeaveDetailRepository EmployeeLeaveDetails { get; }
        IEmployeeLeaveRepository EmployeeLeaves { get; }
        IEmployeeRepository Employees { get; }
        ILeaveTypeRepository LeaveTypes { get; }
        int Complete();
    }
}
