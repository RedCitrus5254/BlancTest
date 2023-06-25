namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Contracts.Exceptions;
using WebApi.BusinessLogic.Contracts.GetTodoItem;
using Xunit;

public class GetTodoItemAsyncTests
{
    [Fact]
    public async void ShouldReturnSavedTodoItemAsync()
    {
        var sut = SutFactory.Create();

        var todoItemEntity = ObjectsGen.RandomTodoItemEntity();

        await sut.SaveTodoItemAsync(todoItemEntity);

        var expected = new GetTodoItemResponse(
            id: todoItemEntity.Id,
            title: todoItemEntity.Title,
            isCompleted: todoItemEntity.IsCompleted);

        var response = await sut.v1Client.GetTodoItemAsync(
            id: todoItemEntity.Id.ToString());
        var content = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<GetTodoItemResponse>(content);

        actual
            .Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ShouldReturnInvalidModelAsync()
    {
        var sut = SutFactory.Create();

        var actual = await sut.v1Client.GetTodoItemAsync(
            id: "invalid");

        actual
            .Should()
            .BeEquivalentTo(new BadRequestException("InvalidModel"));
    }

    [Fact]
    public async Task ShouldReturnNotFoundAsync()
    {
        var todoItemId = ObjectsGen.RandomTodoItemId();
        var sut = SutFactory.Create();

        var actual = await sut.v1Client.GetTodoItemAsync(
            id: todoItemId.ToString());

        actual
            .Should()
            .BeEquivalentTo(new NotFoundException("NotFound"));
    }
}