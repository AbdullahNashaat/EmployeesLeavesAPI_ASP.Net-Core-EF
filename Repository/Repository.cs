using EmployeesLeavesAPI.Repository.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeesLeavesAPI.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        } 
        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
    }
}
