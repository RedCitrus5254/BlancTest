using Bogus;

namespace WebApi.Tests;

public static class TestsConfiguration
{
    private static Faker faker = new Faker();

    public static string PostgresConnectionString { get; } = $"host=localhost;port=5432;database=homework;username=tester;password=tester;";

    public static string WebApiUrl { get; } = "http://localhost:5000";
}