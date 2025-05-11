using System.Linq.Expressions;

namespace code_quests.Core.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        T? GetByID(int id);

        bool AddEntity(T entity);
        bool DeleteEntity(T entity);
        bool UpdateEntity(T entity);
        IReadOnlyList<T> GetAll();
        T? GetbySpec(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include);
        IReadOnlyList<T> FindAllbySpec(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include);
    }
}
