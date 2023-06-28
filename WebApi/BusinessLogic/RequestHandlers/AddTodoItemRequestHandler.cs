using System;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Contracts.AddTodoItem;
using WebApi.Storage.Contracts.Entities;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi.BusinessLogic.RequestHandlers
{
    public class AddTodoItemRequestHandler
    {
        public AddTodoItemRequestHandler(
            ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public ITodoItemRepository todoItemRepository { get; }

        public async Task<AddTodoItemResponse> HandleAsync(
            AddTodoItemRequest request)
        {
            var entity = new TodoItemEntity(
                Id: Guid.NewGuid(),
                Title: request.Title,
                IsCompleted: false);

            await this.todoItemRepository.AddOrUpdateAsync(entity);

            return new AddTodoItemResponse(
                Id: entity.Id);
        }
    }
}