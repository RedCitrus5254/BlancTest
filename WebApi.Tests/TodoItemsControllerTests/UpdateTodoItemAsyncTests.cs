namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using Newtonsoft.Json;
using WebApi.Storage.Contracts.Entities;
using Xunit;
using static WebApi.Engine.ErrorFilter;

public class UpdateTodoItemAsyncTests
{
    [Fact]
    public async Task ShouldUpdateTodoItemAsync()
    {
        var sut = SutFactory.Create();
        var todoItemEntity = ObjectsGen.RandomTodoItemEntity();
        var newTitle = ObjectsGen.RandomTitle();

        await sut.SaveTodoItemAsync(todoItemEntity);

        await sut.v1Client.UpdateTodoItemAsync(
            id: todoItemEntity.Id.ToString(),
            title: newTitle,
            isCompleted: todoItemEntity.IsCompleted);

        await sut.WaitForConsumeEvent();

        var expected = new TodoItemEntity(
            Id: todoItemEntity.Id,
            Title: newTitle,
            IsCompleted: todoItemEntity.IsCompleted);

        var actual = await sut.GetTodoItemAsync(
            id: todoItemEntity.Id);

        actual
            .Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldReturnInvalidModelAsync()
    {
        var sut = SutFactory.Create();

        var response = await sut.v1Client.UpdateTodoItemAsync(
            id: ObjectsGen.RandomTodoItemId().ToString(),
            title: string.Empty,
            isCompleted: false);

        var content = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<ErrorData>(content);

        var expected = new ErrorData(
            userMessage: $"Некорректная модель, код ошибки InvalidModel",
            errorCode: "InvalidModel");

        actual
            .Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldReturnNotFoundAsync()
    {
        var sut = SutFactory.Create();
        var todoItemEntity = ObjectsGen.RandomTodoItemEntity();

        var response = await sut.v1Client.UpdateTodoItemAsync(
            id: todoItemEntity.Id.ToString(),
            title: todoItemEntity.Title,
            isCompleted: todoItemEntity.IsCompleted);

        var content = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<ErrorData>(content);

        var expected = new ErrorData(
            userMessage: $"Запрашиваемый ресурс не найден, код ошибки NotFound",
            errorCode: "NotFound");

        actual
            .Should()
            .BeEquivalentTo(expected);
    }
}