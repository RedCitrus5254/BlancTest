namespace WebApi.Tests;

using WebApi.Storage.Contracts.Entities;
using WebApi.Storage.Contracts.Repositories;

internal class Sut
{
    private readonly ITodoItemRepository todoItemRepository;

    public Sut(
        V1Client v1Client,
        ITodoItemRepository todoItemRepository)
    {
        this.v1Client = v1Client;
        this.todoItemRepository = todoItemRepository;
    }

    public V1Client v1Client { get; private set; }

    internal Task<TodoItemEntity?> GetTodoItemAsync(
        Guid id)
    {
        return this.todoItemRepository.GetAsync(
            id: id);
    }

    internal Task SaveTodoItemAsync(
        TodoItemEntity todoItemEntity)
    {
        return this.todoItemRepository.AddOrUpdateAsync(
            entity: todoItemEntity);
    }
}