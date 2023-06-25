namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using WebApi.BusinessLogic.Contracts.Exceptions;
using WebApi.Storage.Contracts.Entities;
using Xunit;

public class UpdateTodoItemAsyncTests
{
    [Fact]
    public async Task ShouldUpdateTodoItemAsync()
    {
        var sut = SutFactory.Create();
        var todoItemEntity = ObjectsGen.RandomTodoItemEntity();

        await sut.SaveTodoItemAsync(todoItemEntity);

        await sut.v1Client.UpdateTodoItemAsync(
            id: todoItemEntity.Id.ToString(),
            title: todoItemEntity.Title,
            isCompleted: todoItemEntity.IsCompleted.ToString());

        var expected = new TodoItemEntity()
        {
            Id = todoItemEntity.Id,
            Title = todoItemEntity.Title,
            IsCompleted = todoItemEntity.IsCompleted
        };

        var actual = sut.GetTodoItemAsync(
            id: todoItemEntity.Id);

        actual
            .Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldReturnInvalidModelAsync()
    {
        var sut = SutFactory.Create();

        var actual = await sut.v1Client.UpdateTodoItemAsync(
            id: ObjectsGen.RandomTodoItemId().ToString(),
            title: ObjectsGen.RandomTitle(),
            isCompleted: "invalid");

        actual
            .Should()
            .BeEquivalentTo(new InvalidModelException("InvalidModel"));
    }

    [Fact]
    public async Task ShouldReturnNotFoundAsync()
    {
        var sut = SutFactory.Create();
        var todoItemEntity = ObjectsGen.RandomTodoItemEntity();

        var actual = await sut.v1Client.UpdateTodoItemAsync(
            id: todoItemEntity.Id.ToString(),
            title: todoItemEntity.Title,
            isCompleted: todoItemEntity.IsCompleted.ToString());

        actual
            .Should()
            .BeEquivalentTo(new NotFoundException("NotFound"));
    }
}