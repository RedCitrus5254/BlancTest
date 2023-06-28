using System;

namespace WebApi.Queue.Contracts
{
    public record UpdateTodoItemMessage(
            Guid Id,
            string Title,
            bool IsCompleted);
}