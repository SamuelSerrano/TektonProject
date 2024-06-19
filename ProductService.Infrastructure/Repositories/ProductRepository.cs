using ProductService.Domain.Entities;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Repositories.Interfaces;

public class ProductRepository : Repository<Product>, IProductRepository
{
	private readonly ProductDbContext _context;

	public ProductRepository(ProductDbContext context) : base(context)
	{
		_context = context;
	}

}
