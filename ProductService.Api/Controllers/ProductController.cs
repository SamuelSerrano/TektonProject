using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Features.Commands;
using ProductService.Application.Features.Queries;
using ProductService.Application.Response;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
	private readonly IMediator _mediator;

	public ProductController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[SwaggerOperation(Summary = "Crear un producto")]
	[ProducesResponseType(typeof(ProductResponse), 201)]
	public async Task<IActionResult> CreateProduct(CreateProductCommand command)
	{
		var result = await _mediator.Send(command);
		return CreatedAtAction(nameof(GetProductById), new { id = result.ProductId }, result);
	}

	[HttpPut]
	[SwaggerOperation(Summary = "Actualizar un producto existente")]
	[ProducesResponseType(typeof(ProductResponse), 200)]
	public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
	{
		var result = await _mediator.Send(command);
		return Ok(result);
	}

	[HttpGet("{id}")]
	[SwaggerOperation(Summary = "Obtener producto por ProductId")]
	[ProducesResponseType(typeof(ProductResponse), 200)]
	public async Task<IActionResult> GetProductById(int id)
	{
		var result = await _mediator.Send(new GetProductByIdQuery { ProductId = id });
		return Ok(result);
	}
}
