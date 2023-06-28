using System;
using System.Collections.Generic;

namespace WebApi.BusinessLogic.Contracts.GetTodoItemList
{
    public record GetTodoItemListResponse(
        List<GetTodoItemListElement> Items);

    public record GetTodoItemListElement(
        Guid Id,
        string Title,
        bool IsCompleted);
}