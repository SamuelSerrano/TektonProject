using ProductService.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ProductDbContext _db;
		public IProductRepository Product { get; private set; }

		public UnitOfWork(ProductDbContext db, IProductRepository product)
		{
			_db = db;
			Product = product;
		}
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
