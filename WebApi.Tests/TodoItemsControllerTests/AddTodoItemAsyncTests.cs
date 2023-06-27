namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Contracts.AddTodoItem;
using Xunit;
using static WebApi.Engine.ErrorFilter;

public class AddTodoItemAsyncTests
{
    [Fact]
    public async Task ShouldAddTodoItemAsync()
    {
        var sut = await SutFactory.CreateAsync();

        var title = ObjectsGen.RandomTitle();

        var response = await sut.v1Client.SaveTodoItemAsync(
            title: title);
        var content = await response.Content.ReadAsStringAsync();

        var savedTodoItemId = JsonConvert.DeserializeObject<AddTodoItemResponse>(content);

        var savedTodoItem = await sut.GetTodoItemAsync(
            id: savedTodoItemId.Id);

        savedTodoItem!
            .Title
            .Should()
            .BeEquivalentTo(title);
    }

    [Fact]
    public async Task ShouldReturnInvalidModelAsync()
    {
        var sut = await SutFactory.CreateAsync();

        var response = await sut.v1Client.SaveTodoItemAsync(
            title: string.Empty);

        var content = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<ErrorData>(content);

        var expected = new ErrorData(
            userMessage: $"Некорректная модель, код ошибки InvalidModel",
            errorCode: "InvalidModel");

        actual
            .Should()
            .BeEquivalentTo(expected);
    }
}