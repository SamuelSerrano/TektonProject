using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Extensions;
using ProductService.Api.Middleware;
using ProductService.Application.Map;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region AutoMapper
var mapperConfig = new MapperConfiguration(mp => 
{
	mp.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();
#endregion

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope()) 
{ 
	var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
	context.Database.Migrate();
}
	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
