using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Response
{
	public class ProductResponse
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public int Status { get; set; }
		public string StatusName { get; set; }
		public int Stock { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; } 
		public decimal FinalPrice { get; set; } 

		
		public ProductResponse(int productId, string name, int status, string statusName, int stock, string description, decimal price, decimal discount)
		{
			ProductId = productId;
			Name = name;
			Status = status;
			StatusName = statusName;
			Stock = stock;
			Description = description;
			Price = price;
			Discount = discount;
			FinalPrice = price * (100 - discount) / 100;
		}

		
		public ProductResponse() { }
	}
}
