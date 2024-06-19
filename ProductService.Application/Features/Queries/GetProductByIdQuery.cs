using MediatR;
using ProductService.Application.Response;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Application.Features.Queries
{
    public class GetProductByIdQuery: IRequest<ProductResponse>
    {
		[Required(ErrorMessage = "ProductId es requerido")]
		[Range(1, int.MaxValue, ErrorMessage = "ProductId debe ser un número entero positivo")]
		public int ProductId { get; set; }
	}
}
