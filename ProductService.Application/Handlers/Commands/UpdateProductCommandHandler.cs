using MediatR;
using ProductService.Application.Features.Commands;
using ProductService.Application.Interfaces;
using ProductService.Application.Response;
using ProductService.Domain.Entities;

namespace ProductService.Application.Handlers.Commands
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
	{
		private readonly IProductService _productService;

		public UpdateProductCommandHandler(IProductService productService)
		{
			_productService = productService;
		}

		public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var product = new Product
			{
				Name = request.Name,
				Status = request.Status,
				Stock = request.Stock,
				Description = request.Description,
				Price = request.Price
			};

			return await _productService.UpdateProduct(request.ProductId, product);
		}
	}
}
