﻿using AutoMapper;
using Azure.Core;
using MediatR;
using ProductService.Application.Interfaces;
using ProductService.Application.Response;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Repositories.Interfaces;

namespace ProductService.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork? _unitOfWork;
		private readonly ICacheService? _cacheService;
		private readonly IDiscountService? _discountService;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, ICacheService cacheService, IDiscountService discountService, IMapper mapper)
		{
			_cacheService = cacheService;
			_unitOfWork = unitOfWork;
			_discountService = discountService;
			_mapper = mapper;
		}

		async Task<ProductResponse> IProductService.CreateProduct(Product Producto)
		{
			await _unitOfWork.Product.AddAsync(Producto);
			await _unitOfWork.SaveAsync();
			return await ArmarRespuesta(Producto);			
		}

		async Task<ProductResponse> IProductService.UpdateProduct(int ProductId, Product request)
		{
			var product = await _unitOfWork.Product.GetAsync(x => x.ProductId == ProductId);
			if (product == null)
			{
				throw new KeyNotFoundException($"Product with ID {ProductId} not found.");
			}

			product.Name = request.Name;
			product.Status = request.Status;
			product.Stock = request.Stock;
			product.Description = request.Description;
			product.Price = request.Price;

			await _unitOfWork.Product.UpdateAsync(product);
			await _unitOfWork.SaveAsync();

			return await ArmarRespuesta(product);			
		}

		async Task<ProductResponse> IProductService.GetProductById(int ProductId)
		{
			var product = await _unitOfWork.Product.GetAsync(x => x.ProductId == ProductId) ?? throw new KeyNotFoundException($"Product with ID {ProductId} not found.");
			return await ArmarRespuesta(product);
		}

		private async Task<ProductResponse> ArmarRespuesta(Product Producto)
		{
			var statusName = _cacheService.GetStatusName(Producto.Status);
			var discount = await _discountService.GetDiscountAsync(Producto.ProductId);

			ProductResponse productResponse = _mapper.Map<ProductResponse>(Producto);
			productResponse.StatusName = statusName;
			productResponse.Discount = discount;
			productResponse.FinalPrice = Producto.Price * (100 - discount) / 100;
			return productResponse;
		}
	}
}
