using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;

public class ProductDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }

	public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>().ToTable("Product");
		modelBuilder.Entity<Product>()
		.Property(p => p.Price)
		.HasColumnType("decimal(18, 2)");
	}
}
