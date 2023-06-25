namespace WebApi.BusinessLogic.Contracts.AddTodoItem
{
    public class AddTodoItemRequest
    {
        public AddTodoItemRequest(
            string Title)
        {
            this.Title = Title;
        }

        public string Title { get; }
    }
}