using ProductService.Application.Response;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces
{
	public interface IProductService
	{
		public Task<ProductResponse> CreateProduct(Product Producto);
		public Task<ProductResponse> UpdateProduct(int ProductId, Product Producto);
		public Task<ProductResponse> GetProductById(int ProductId);
	}
}
