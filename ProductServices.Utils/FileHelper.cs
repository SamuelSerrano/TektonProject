namespace ProductServices.Utils
{
	public static class FileHelper
	{
		public static async Task LogToFileAsync(string message)
		{
			var logFilePath = "logs/request_logs.txt";

			// Ensure the directory exists
			var logDirectory = Path.GetDirectoryName(logFilePath);
			if (!Directory.Exists(logDirectory))
			{
				Directory.CreateDirectory(logDirectory);
			}

			await using var writer = new StreamWriter(logFilePath, append: true);
			await writer.WriteLineAsync(message);
		}
	}
}
