using EmployeesLeavesAPI.Contexts;
using EmployeesLeavesAPI.Models;

using EmployeesLeavesAPI.Repository.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeesLeavesAPI.Repository
{
    public class EmpolyeeRpository : Repository<Employee>, IEmployeeRepository
    {
        public EmpolyeeRpository(EmloyeeLeavesContext context) : base(context)
        {
        }
        public EmloyeeLeavesContext EmloyeeLeavesContext
        {
            get { return Context as EmloyeeLeavesContext; }
        }
    }
}
