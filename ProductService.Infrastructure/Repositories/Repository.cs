using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ProductDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ProductDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();			
		}
		public async Task AddAsync(T entity)
		{
			await this.dbSet.AddAsync(entity);
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			return await query.FirstOrDefaultAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			dbSet.Update(entity);
		}
	}
}
