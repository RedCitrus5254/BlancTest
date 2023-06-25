namespace WebApi.Tests;

using System;
using Bogus;
using WebApi.Storage.Contracts.Entities;

internal static class ObjectsGen
{
    private static Faker faker = new Faker();

    internal static string RandomTitle()
    {
        throw new NotImplementedException();
    }

    internal static TodoItemEntity RandomTodoItemEntity()
    {
        return new TodoItemEntity()
        {
            Id = RandomTodoItemId(),
            Title = faker.Random.String(),
            IsCompleted = faker.Random.Bool()
        };
    }

    internal static Guid RandomTodoItemId()
    {
        return Guid.NewGuid();
    }
}