using MediatR;
using ProductService.Application.Features.Queries;
using ProductService.Application.Interfaces;
using ProductService.Application.Response;
using ProductService.Domain.Entities;

namespace ProductService.Application.Handlers.Queries
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
	{
		IProductService _productService;
        public GetProductByIdQueryHandler(IProductService productService)
        {
			_productService = productService;
        }
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			return await _productService.GetProductById(request.ProductId);
		}
	}
}
