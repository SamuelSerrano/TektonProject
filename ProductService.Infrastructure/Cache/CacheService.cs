using Microsoft.Extensions.Caching.Memory;

public class CacheService : ICacheService
{
	private readonly IMemoryCache _cache;

	public CacheService(IMemoryCache cache)
	{
		_cache = cache;
	}

	public string GetStatusName(int status)
	{
		var statusDict = _cache.GetOrCreate("StatusDictionary", entry =>
		{
			entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
			return new Dictionary<int, string>
			{
				{ 1, "Active" },
				{ 0, "Inactive" }
			};
		});

		return statusDict.ContainsKey(status) ? statusDict[status] : "Unknown";
	}
}
