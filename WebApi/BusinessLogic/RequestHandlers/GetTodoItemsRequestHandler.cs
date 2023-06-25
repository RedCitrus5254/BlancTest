using System.Linq;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Contracts.GetTodoItemList;
using WebApi.Storage.Contracts.Entities;
using WebApi.Storage.Contracts.Repositories;

namespace WebApi.BusinessLogic.RequestHandlers
{
    public class GetTodoItemsRequestHandler
    {
        private readonly ITodoItemRepository todoItemRepository;

        public GetTodoItemsRequestHandler(
            ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public async Task<GetTodoItemListResponse> HandleAsync()
        {
            var entities = await this.todoItemRepository.GetAllAsync();

            return new GetTodoItemListResponse(
                Items: entities.Select(Map).ToList());
        }

        private static GetTodoItemListElement Map(
            TodoItemEntity entity)
        {
            return new GetTodoItemListElement(
                Id: entity.Id,
                Title: entity.Title,
                IsCompleted: entity.IsCompleted);
        }
    }
}