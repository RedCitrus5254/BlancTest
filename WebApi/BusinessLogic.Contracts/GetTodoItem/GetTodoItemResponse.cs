using System;

namespace WebApi.BusinessLogic.Contracts.GetTodoItem
{
    public record GetTodoItemResponse(
            Guid Id,
            string Title,
            bool IsCompleted);
}