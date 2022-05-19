using System.Linq.Expressions;

namespace EmployeesLeavesAPI.Repository.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
    }
}
