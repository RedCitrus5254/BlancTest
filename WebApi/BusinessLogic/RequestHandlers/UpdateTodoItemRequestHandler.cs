using System;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Contracts.UpdateTodoItem;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi.BusinessLogic.RequestHandlers
{
    public class UpdateTodoItemRequestHandler
    {
        private readonly ITodoItemRepository todoItemRepository;

        public UpdateTodoItemRequestHandler(
            ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public Task HandleAsync(
            Guid id,
            UpdateTodoItemRequest request)
        {
            return this.todoItemRepository.AddOrUpdateAsync(
                new Storage.Contracts.Entities.TodoItemEntity
                {
                    Id = id,
                    Title = request.Title,
                    IsCompleted = request.IsCompleted,
                });
        }
    }
}