using AutoMapper;
using ProductService.Application.Response;
using ProductService.Domain.Entities;

namespace ProductService.Application.Map
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>(); 
        }
    }
}
