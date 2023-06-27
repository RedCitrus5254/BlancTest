namespace WebApi.BusinessLogic.RequestHandlers
{
    using System;
    using System.Threading.Tasks;
    using WebApi.BusinessLogic.Contracts.Exceptions;
    using WebApi.BusinessLogic.Contracts.UpdateTodoItem;
    using WebApi.Storage.Contracts.Repositories;

    public class UpdateTodoItemRequestHandler
    {
        private readonly ITodoItemRepository todoItemRepository;

        public UpdateTodoItemRequestHandler(
            ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public async Task HandleAsync(
            Guid id,
            UpdateTodoItemRequest request)
        {
            var entity = await this.todoItemRepository.GetAsync(id: id);

            if (entity == null)
            {
                throw new NotFoundException(errorCode: "NotFound");
            }

            await this.todoItemRepository.AddOrUpdateAsync(
                new Storage.Contracts.Entities.TodoItemEntity
                {
                    Id = id,
                    Title = request.Title,
                    IsCompleted = request.IsCompleted,
                });
        }
    }
}