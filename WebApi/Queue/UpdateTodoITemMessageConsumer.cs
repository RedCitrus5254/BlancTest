using System.Threading.Tasks;
using MassTransit;
using WebApi.Queue.Contracts;
using WebApi.Storage.Contracts.Entities;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi.Queue
{
    public class UpdateTodoITemMessageConsumer : IConsumer<UpdateTodoItemMessage>
    {
        private readonly ITodoItemRepository todoItemRepository;

        public UpdateTodoITemMessageConsumer(
            ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public Task Consume(
            ConsumeContext<UpdateTodoItemMessage> context)
        {
            var entity = new TodoItemEntity(
                Id: context.Message.Id,
                Title: context.Message.Title,
                IsCompleted: context.Message.IsCompleted
            );

            return this.todoItemRepository
                .AddOrUpdateAsync(
                    entity: entity);
        }
    }
}