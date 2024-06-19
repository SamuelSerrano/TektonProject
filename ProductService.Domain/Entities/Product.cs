
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Domain.Entities
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductId { get; set; }
		public string Name { get; set; }
		public int Status { get; set; } 
		public int Stock { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
				
		
		public Product() { }
	}
}
