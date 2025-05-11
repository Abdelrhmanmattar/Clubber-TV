using code_quests.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace code_quests.EF.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DbContext context;

        public BaseRepo(DbContext _context)
        {
            context = _context;
        }
        public T? GetByID(int id) =>
                                    context.Set<T>().Find(id);
        public IReadOnlyList<T> GetAll() =>
                                    context.Set<T>().AsNoTracking().ToList();

        public T? GetbySpec(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include)
        {
            IQueryable<T> value = null;
            if (Criteria != null)
            {
                value = context.Set<T>().Where(Criteria);
            }
            if (Include != null && value != null)
            {
                value = value.Include(Include);
            }
            return value.ToList().FirstOrDefault();

        }
        public IReadOnlyList<T> FindAllbySpec(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include)
        {
            IQueryable<T> value = null;
            if (Criteria != null)
            {
                value = context.Set<T>().Where(Criteria);
            }
            if (Include != null && value != null)
            {
                value = value.Include(Include);
            }
            return value.ToList().AsReadOnly();

        }

        public bool AddEntity(T entity)
        {
            var feedback = context.Set<T>().Add(entity);
            return feedback.State == EntityState.Added;
        }
        public bool DeleteEntity(T entity)
        {
            var feedback = context.Set<T>().Remove(entity);
            return feedback.State == EntityState.Deleted;
        }
        public bool UpdateEntity(T entity)
        {
            var state = context.Set<T>().Update(entity);
            return state.State == EntityState.Modified;
        }
    }
}
