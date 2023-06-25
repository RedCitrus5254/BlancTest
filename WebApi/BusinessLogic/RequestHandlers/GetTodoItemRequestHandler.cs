using System;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Contracts.Exceptions;
using WebApi.BusinessLogic.Contracts.GetTodoItem;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi.BusinessLogic.RequestHandlers
{
    public class GetTodoItemRequestHandler
    {
        private readonly ITodoItemRepository todoItemRepository;

        public GetTodoItemRequestHandler(
            ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public async Task<GetTodoItemResponse> HandleAsync(
            Guid id)
        {
            var entity = await this.todoItemRepository.GetAsync(id: id);

            if (entity == null)
            {
                throw new NotFoundException(errorCode: "NotFound");
            }

            return new GetTodoItemResponse(
                id: entity.Id,
                title: entity.Title,
                isCompleted: entity.IsCompleted);
        }
    }
}