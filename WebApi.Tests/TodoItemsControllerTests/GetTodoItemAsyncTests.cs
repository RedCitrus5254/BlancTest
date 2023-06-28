namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Contracts.GetTodoItem;
using Xunit;
using static WebApi.Engine.ErrorFilter;

public class GetTodoItemAsyncTests
{
    [Fact]
    public async void ShouldReturnSavedTodoItemAsync()
    {
        var sut = SutFactory.Create();

        var todoItemEntity = ObjectsGen.RandomTodoItemEntity();

        await sut.SaveTodoItemAsync(todoItemEntity);

        var expected = new GetTodoItemResponse(
            Id: todoItemEntity.Id,
            Title: todoItemEntity.Title,
            IsCompleted: todoItemEntity.IsCompleted);

        var response = await sut.v1Client.GetTodoItemAsync(
            id: todoItemEntity.Id.ToString());
        var content = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<GetTodoItemResponse>(content);

        actual
            .Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldReturnNotFoundAsync()
    {
        var todoItemId = ObjectsGen.RandomTodoItemId();
        var sut = SutFactory.Create();

        var response = await sut.v1Client.GetTodoItemAsync(
            id: todoItemId.ToString());

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