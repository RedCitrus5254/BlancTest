namespace WebApi.Tests;

public static class TestsConfiguration
{
    public static string PostgresConnectionString { get; } = $"host=localhost;port=5432;database=homework;username=tester;password=tester;";

}