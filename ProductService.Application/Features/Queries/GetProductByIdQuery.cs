using MediatR;
using ProductService.Application.Response;

namespace ProductService.Application.Features.Queries
{
    public class GetProductByIdQuery: IRequest<ProductResponse>
    {
		public int ProductId { get; set; }
	}
}
