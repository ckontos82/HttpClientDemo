using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace HttpClientDemo
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Demo HTTP Client\n");

			await GetAsync(sharedClient);
			await GetFromJsonAsync(sharedClient);
			await PostAsync(sharedClient);
		}

		private static HttpClient sharedClient = new()
		{
			BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
		};

		static async Task GetAsync(HttpClient httpClient)
		{
			using HttpResponseMessage response = await httpClient.GetAsync("todos/3");

			response.EnsureSuccessStatusCode()
				.WriteRequestToConsole();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			await Console.Out.WriteLineAsync($"{jsonResponse}\n");
		}

		static async Task GetFromJsonAsync(HttpClient httpClient)
		{
			var todos = await httpClient.GetFromJsonAsync<List<Todo>>(
				"todos?userId=1&completed=true");

			todos?.ForEach(Console.WriteLine);
			await Console.Out.WriteLineAsync();
		}

		static async Task PostAsync(HttpClient httpClient)
		{
			using StringContent jsonContent = new(
				JsonSerializer.Serialize(
					new
					{
						userId = 77,
						id = 1,
						title = "Posting Sample",
						completed = false
					}),
					Encoding.UTF8,
					"application/json"
			);

			using HttpResponseMessage response = await httpClient.PostAsync(
				"todos",
				jsonContent
			);

			response.EnsureSuccessStatusCode()
				.WriteRequestToConsole();
		}

	}
}
