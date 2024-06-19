using MediatR;
using ProductService.Application.Response;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Features.Commands
{
    public class UpdateProductCommand: IRequest<ProductResponse>
    {
		[Required(ErrorMessage = "ProductId es requerido")]
		[Range(1, int.MaxValue, ErrorMessage = "ProductId debe ser un número entero positivo")]
		public int ProductId { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Nombre Obligatorio")]
		public string? Name { get; set; }
		[Range(0, 1, ErrorMessage = "Estado debe ser un valor de 0 o 1")]
		public int Status { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
		public int Stock { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Description Obligatorio")]
		public string? Description { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
		public decimal Price { get; set; }
	}
}
