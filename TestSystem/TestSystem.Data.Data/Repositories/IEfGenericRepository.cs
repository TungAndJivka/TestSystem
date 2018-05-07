using System.Linq;
using TestSystem.Data.Models.Abstractions;

namespace TestSystem.Data.Data.Repositories
{
    public interface IEfGenericRepository<T> 
        where T : class, IDeletable
    {
        IQueryable<T> All { get; }

        void Add(T entity);

        void Delete(T entity);

        void RealDelete(T entity);

        void Update(T entity);
    }
}