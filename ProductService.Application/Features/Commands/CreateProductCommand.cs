using MediatR;
using ProductService.Application.Response;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Features.Commands
{
	public class CreateProductCommand : IRequest<ProductResponse>
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Nombre Obligatorio")]
		public string? Name { get; set; }
		public int Status { get; set; }
		public int Stock { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Description Obligatorio")]
		public string? Description { get; set; }
		public decimal Price { get; set; }
	}
}
