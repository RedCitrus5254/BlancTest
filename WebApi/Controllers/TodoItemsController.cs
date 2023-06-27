using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.BusinessLogic.Contracts.AddTodoItem;
using WebApi.BusinessLogic.Contracts.Exceptions;
using WebApi.BusinessLogic.Contracts.GetTodoItem;
using WebApi.BusinessLogic.Contracts.GetTodoItemList;
using WebApi.BusinessLogic.Contracts.UpdateTodoItem;
using WebApi.BusinessLogic.RequestHandlers;
using WebApi.Engine;

namespace WebApi.Controllers
{
    [ApiController]
    [ErrorFilter]
    [Route("todoItems")]
    public class TodoItemsController : ControllerBase
    {
        private readonly GetTodoItemRequestHandler getTodoItemRequestHandler;
        private readonly GetTodoItemsRequestHandler getTodoItemsRequestHandler;
        private readonly AddTodoItemRequestHandler addTodoItemRequestHandler;
        private readonly UpdateTodoItemRequestHandler updateTodoItemRequestHandler;

        public TodoItemsController(
            GetTodoItemRequestHandler getTodoItemRequestHandler,
            GetTodoItemsRequestHandler getTodoItemsRequestHandler,
            AddTodoItemRequestHandler addTodoItemRequestHandler,
            UpdateTodoItemRequestHandler updateTodoItemRequestHandler)
        {
            this.getTodoItemRequestHandler = getTodoItemRequestHandler;
            this.getTodoItemsRequestHandler = getTodoItemsRequestHandler;
            this.addTodoItemRequestHandler = addTodoItemRequestHandler;
            this.updateTodoItemRequestHandler = updateTodoItemRequestHandler;
        }

        [HttpGet("{id:guid}")]
        public Task<GetTodoItemResponse> GetTodoItemAsync(Guid id)
        {
            return getTodoItemRequestHandler.HandleAsync(id);
        }

        [HttpGet]
        public Task<GetTodoItemListResponse> GetTodoItemListAsync()
        {
            return getTodoItemsRequestHandler.HandleAsync();
        }

        [HttpPost]
        public Task<AddTodoItemResponse> AddTodoItemAsync([FromBody] AddTodoItemRequest request)
        {
            if (request.Title == string.Empty)
            {
                throw new InvalidModelException(
                    errorCode: "InvalidModel");
            }

            return addTodoItemRequestHandler.HandleAsync(request);
        }

        [HttpPut("{id:guid}")]
        public async Task TaskUpdateTodoItemAsync(Guid id, [FromBody] UpdateTodoItemRequest request)
        {
            if (request.Title == string.Empty)
            {
                throw new InvalidModelException(
                    errorCode: "InvalidModel");
            }

            await updateTodoItemRequestHandler.HandleAsync(id, request);
        }
    }
}