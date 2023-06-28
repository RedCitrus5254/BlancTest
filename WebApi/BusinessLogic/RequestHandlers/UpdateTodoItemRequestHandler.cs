namespace WebApi.BusinessLogic.RequestHandlers
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using WebApi.BusinessLogic.Contracts.Exceptions;
    using WebApi.BusinessLogic.Contracts.UpdateTodoItem;
    using WebApi.Queue.Contracts;
    using WebApi.Storage.Contracts.Repositories;

    public class UpdateTodoItemRequestHandler
    {
        private readonly ITodoItemRepository todoItemRepository;
        private readonly IBus bus;

        public UpdateTodoItemRequestHandler(
            ITodoItemRepository todoItemRepository,
            IBus bus)
        {
            this.todoItemRepository = todoItemRepository;
            this.bus = bus;
        }

        public async Task HandleAsync(
            Guid id,
            UpdateTodoItemRequest request)
        {
            var savedEntity = await this.todoItemRepository.GetAsync(id: id);

            if (savedEntity == null)
            {
                throw new NotFoundException(errorCode: "NotFound");
            }

            var entityToUpdate = new UpdateTodoItemMessage(
                Id: id,
                Title: request.Title,
                IsCompleted: request.IsCompleted);

            await this.bus.Publish(entityToUpdate);
        }
    }
}