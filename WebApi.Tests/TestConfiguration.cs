namespace WebApi.Tests;

public static class TestsConfiguration
{
    public static string PostgresConnectionString { get; } = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

    public static string WebApiUrl { get; } = "https://localhost";
}