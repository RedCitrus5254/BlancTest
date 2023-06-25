namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Contracts.AddTodoItem;
using WebApi.BusinessLogic.Contracts.Exceptions;
using Xunit;

public class AddTodoItemAsyncTests
{
    [Fact]
    public async Task ShouldAddTodoItemAsync()
    {
        var sut = SutFactory.Create();
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
        var sut = SutFactory.Create();

        var actual = await sut.v1Client.SaveTodoItemAsync(
            title: null);

        actual
            .Should()
            .BeEquivalentTo(new InvalidModelException("InvalidModel"));
    }
}