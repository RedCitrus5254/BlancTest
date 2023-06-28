namespace WebApi.Tests;

using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi.Queue.Contracts;
using WebApi.Storage.Contracts.Entities;
using WebApi.Storage.Contracts.Repositories;

internal class Sut
{
    private readonly WebApplicationFactory<Program> webApplicationFactory;
    private readonly ITodoItemRepository todoItemRepository;

    public Sut(
        WebApplicationFactory<Program> webApplicationFactory,
        V1Client v1Client,
        ITodoItemRepository todoItemRepository)
    {
        this.webApplicationFactory = webApplicationFactory;
        this.webApplicationFactory = webApplicationFactory;
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

    internal async Task WaitForConsumeEvent()
    {
        var harness = this.webApplicationFactory.Services.GetTestHarness();

        bool res = false;
        while (res == false)
        {
            res = await harness.Consumed.Any<UpdateTodoItemMessage>();
        }
    }
}