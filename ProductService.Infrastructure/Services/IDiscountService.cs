// ProductService.Infrastructure/Services/DiscountService.cs
public interface IDiscountService
{
	public Task<decimal> GetDiscountAsync(int productId);
}