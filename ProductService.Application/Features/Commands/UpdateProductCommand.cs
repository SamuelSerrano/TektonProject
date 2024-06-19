using MediatR;
using ProductService.Application.Response;

namespace ProductService.Application.Features.Commands
{
    public class UpdateProductCommand: IRequest<ProductResponse>
    {
		public int ProductId { get; set; }
		public string? Name { get; set; }
		public int Status { get; set; }
		public int Stock { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
	}
}
