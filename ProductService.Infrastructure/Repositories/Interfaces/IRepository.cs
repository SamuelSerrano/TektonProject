using System.Linq.Expressions;

namespace ProductService.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
		Task AddAsync(T entity);
		Task<T> GetAsync(Expression<Func<T, bool>> filter);
		Task UpdateAsync(T entity);

	}
}