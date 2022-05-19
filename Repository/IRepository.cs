using System.Linq.Expressions;

namespace EmployeesLeavesAPI.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>>predicate);
        TEntity FindById(int id);
    }
}
