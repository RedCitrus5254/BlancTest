using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApi.Storage;

namespace WebApi.Tests;

internal static class SutFactory
{
    public static Sut Create()
    {
        var args = new[]
        {
            $"ConnectionStrings:postgres={TestsConfiguration.PostgresConnectionString}",
        };
        Task.Run(() => Program.Main(args));

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(TestsConfiguration.WebApiUrl)
        };

        return new Sut(
            v1Client: new V1Client(httpClient),
            todoItemRepository: new TodoItemRepository(
                postgresConnectionString: TestsConfiguration.PostgresConnectionString));
    }
}