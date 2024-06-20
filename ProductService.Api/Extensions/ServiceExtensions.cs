using FluentValidation;
using ProductService.Application.Interfaces;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Repositories.Interfaces;
using System.Reflection;

namespace ProductService.Api.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			#region Application
			services.AddScoped<IProductService, Application.Services.ProductService>();
			var applicationAssembly = Assembly.Load("ProductService.Application");
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
			#endregion

			#region Ïnfraestructura
			services.AddScoped<ICacheService, CacheService>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IProductRepository, ProductRepository>();

			services.AddHttpClient<IDiscountService, DiscountService>(client =>
			{
				client.BaseAddress = new Uri(configuration["ExternalApiSettings:BaseUrl"]);
				client.DefaultRequestHeaders.Add("Accept", "application/json");
			});

			services.AddMemoryCache();
			#endregion
						
			
			
			return services;
		}
	}
}
