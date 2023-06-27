using System.Net.Sockets;
using WebApi.Storage;

namespace WebApi.Tests;

internal static class SutFactory
{
    private static readonly Task WhenSetupComplete = SetupAsync();

    public static async Task<Sut> CreateAsync()
    {
        await WhenSetupComplete;

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(TestsConfiguration.WebApiUrl)
        };

        return new Sut(
            v1Client: new V1Client(httpClient),
            todoItemRepository: new TodoItemRepository(
                postgresConnectionString: TestsConfiguration.PostgresConnectionString));
    }

    private static async Task SetupAsync()
    {
        var args = new[]
        {
            $"ConnectionStrings:postgres={TestsConfiguration.PostgresConnectionString}",
        };

        Task.Run(() => Program.Main(args));

        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        await WaitApiUpAsync(
            url: "http://localhost:5000/todoitems",
            cancellationToken: cts.Token);
    }

    private static async Task WaitApiUpAsync(
        string url,
        CancellationToken cancellationToken)
    {
        using var client = new HttpClient();

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var response = await client.GetAsync(
                    requestUri: url,
                    cancellationToken: cancellationToken);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"ExternalService {url} is UP");
                    return;
                }
            }
            catch (HttpRequestException exception) when (exception.InnerException is SocketException)
            {
                // ignore connection refused exception
            }

            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
        }
    }
}