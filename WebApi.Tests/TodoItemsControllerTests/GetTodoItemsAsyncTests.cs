namespace WebApi.Tests.TodoItemsControllerTests;

using FluentAssertions;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Contracts.GetTodoItemList;
using Xunit;

public class GetTodoItemListAsyncTests
{
    [Fact]
    public async void ShouldReturnSavedTodoItemAsync()
    {
        var sut = SutFactory.Create();

        var todoItemEntity1 = ObjectsGen.RandomTodoItemEntity();
        var todoItemEntity2 = ObjectsGen.RandomTodoItemEntity();

        await sut.SaveTodoItemAsync(todoItemEntity1);
        await sut.SaveTodoItemAsync(todoItemEntity2);

        var expected = new GetTodoItemListResponse(
            Items: new List<GetTodoItemListElement>()
            {
                    new GetTodoItemListElement(
                        Id: todoItemEntity1.Id,
                        Title: todoItemEntity1.Title,
                        IsCompleted: todoItemEntity1.IsCompleted),
                    new GetTodoItemListElement(
                        Id: todoItemEntity2.Id,
                        Title: todoItemEntity2.Title,
                        IsCompleted: todoItemEntity2.IsCompleted),
            });

        var response = await sut.v1Client.GetTodoItemsAsync();
        var content = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<GetTodoItemListResponse>(content);

        actual
            .Should()
            .BeEquivalentTo(expected);
    }
}