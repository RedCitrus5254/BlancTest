namespace WebApi.BusinessLogic.Contracts.UpdateTodoItem
{
    public record UpdateTodoItemRequest(
            string Title,
            bool IsCompleted);
}