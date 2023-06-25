using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Contracts.AddTodoItem;
using WebApi.BusinessLogic.Contracts.GetTodoItem;
using WebApi.BusinessLogic.Contracts.GetTodoItemList;
using WebApi.BusinessLogic.Contracts.UpdateTodoItem;
using WebApi.BusinessLogic.RequestHandlers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("todoItems")]
    public class TodoItemsController : ControllerBase
    {
        private readonly GetTodoItemRequestHandler _getTodoItemRequestHandler;
        private readonly GetTodoItemsRequestHandler _getTodoItemsRequestHandler;
        private readonly AddTodoItemRequestHandler _addTodoItemRequestHandler;

        public TodoItemsController(
            GetTodoItemRequestHandler getTodoItemRequestHandler,
            GetTodoItemsRequestHandler getTodoItemsRequestHandler,
            AddTodoItemRequestHandler addTodoItemRequestHandler
        )
        {
            _getTodoItemRequestHandler = getTodoItemRequestHandler;
            _getTodoItemsRequestHandler = getTodoItemsRequestHandler;
            _addTodoItemRequestHandler = addTodoItemRequestHandler;
        }

        [HttpGet("{id:guid}")]
        public Task<GetTodoItemResponse> GetTodoItemAsync(Guid id)
        {
            return _getTodoItemRequestHandler.HandleAsync(id);
        }

        [HttpGet]
        public Task<GetTodoItemListResponse> GetTodoItemsAsync()
        {
            return _getTodoItemsRequestHandler.HandleAsync();
        }

        [HttpPost]
        public Task<AddTodoItemResponse> AddTodoItemAsync([FromBody] AddTodoItemRequest request)
        {
            return _addTodoItemRequestHandler.HandleAsync(request);
        }

        [HttpPut("{id:guid}")]
        public Task<GetTodoItemResponse> UpdateTodoItemAsync(Guid id, [FromBody] UpdateTodoItemRequest request)
        {
            return this.UpdateTodoItemAsync(id, request);
        }
    }
}