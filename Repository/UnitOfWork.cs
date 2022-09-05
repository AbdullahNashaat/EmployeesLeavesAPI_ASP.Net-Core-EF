using EmployeesLeavesAPI.Contexts;
using EmployeesLeavesAPI.Repository.RepositoryInterfaces;
namespace EmployeesLeavesAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmloyeeLeavesContext _context;

        public UnitOfWork(EmloyeeLeavesContext context)
        {
            _context = context;
            Employees = new EmpolyeeRpository(_context);
            //EmployeeLeaveDetails = new EmployeeLeaveDetailRepository(_context);
            //EmployeeLeaves = new EmployeeLeaveRepository(_context);
            //LeaveTypes = new LeaveTypeRepository(_context);
        }
        public IEmployeeLeaveDetailRepository EmployeeLeaveDetails { get; private set; }

        public IEmployeeLeaveRepository EmployeeLeaves { get; private set; }

        public IEmployeeRepository Employees { get; private set; }

        public ILeaveTypeRepository LeaveTypes { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
