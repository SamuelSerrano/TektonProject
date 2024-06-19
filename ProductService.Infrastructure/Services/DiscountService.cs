using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class DiscountService : IDiscountService
{
	private readonly HttpClient _httpClient;
	private readonly IConfiguration _configuration;

	public DiscountService(HttpClient httpClient, Microsoft.Extensions.Configuration.IConfiguration configuration)
	{
		_httpClient = httpClient;
		_configuration = configuration;
	}

	public async Task<decimal> GetDiscountAsync(int productId)
	{
		var response = await _httpClient.GetStringAsync($"{_configuration["ExternalApiSettings:BaseUrl"]}?id={productId}");
		var jsonArray = JArray.Parse(response);
		var discount = jsonArray.FirstOrDefault()?["discount"]?.Value<decimal>() ?? 0m;

		return discount;
	}
}
