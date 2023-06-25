using System;

namespace WebApi.BusinessLogic.Contracts.GetTodoItem
{
    public class GetTodoItemResponse
    {
        public GetTodoItemResponse(
            Guid id,
            string title,
            bool isCompleted)
        {
            this.Id = id;
            this.Title = title;
            this.IsCompleted = isCompleted;
        }

        public Guid Id { get; }
        public string Title { get; }
        public bool IsCompleted { get; }
    }
}