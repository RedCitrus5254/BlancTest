using Microsoft.AspNetCore.Mvc.Testing;
using WebApi.Storage;
using MassTransit;
using WebApi.Queue;

namespace WebApi.Tests;

internal static class SutFactory
{
    public static Sut Create()
    {
        var args = new[]
        {
            $"ConnectionStrings:postgres={TestsConfiguration.PostgresConnectionString}",
        };

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                    services.AddMassTransitTestHarness(cfg =>
                    {
                        cfg.AddConsumer<UpdateTodoITemMessageConsumer>();
                    })));

        return new Sut(
            webApplicationFactory: application,
            v1Client: new V1Client(application.CreateClient()),
            todoItemRepository: new TodoItemRepository(
                postgresConnectionString: TestsConfiguration.PostgresConnectionString));
    }
}