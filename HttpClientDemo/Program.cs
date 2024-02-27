using System.Net.Http.Json;

namespace HttpClientDemo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Demo HTTP Client");
		}

		private static HttpClient httpClient = new()
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
				"todos?userId=1&completed=false");

			todos?.ForEach(Console.WriteLine);
            await Console.Out.WriteLineAsync();
        }
	}
}
