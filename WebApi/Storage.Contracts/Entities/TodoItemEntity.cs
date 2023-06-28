using System;

namespace WebApi.Storage.Contracts.Entities
{
    public record TodoItemEntity(
        Guid Id,
        string Title,
        bool IsCompleted);
}