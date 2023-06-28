namespace WebApi.Tests;

using System.Text;
using Newtonsoft.Json;
using WebApi.BusinessLogic.Contracts.AddTodoItem;
using WebApi.BusinessLogic.Contracts.UpdateTodoItem;

internal class V1Client
{
    private readonly HttpClient httpClient;

    public V1Client(
        HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    internal async Task<HttpResponseMessage> GetTodoItemAsync(
        string id)
    {
        var request = new HttpRequestMessage(
            method: HttpMethod.Get,
            requestUri: $"todoitems/{id}");

        return await this.httpClient.SendAsync(request);
    }

    internal async Task<HttpResponseMessage> GetTodoItemsAsync()
    {
        var request = new HttpRequestMessage(
            method: HttpMethod.Get,
            requestUri: $"todoitems");

        return await this.httpClient.SendAsync(request);
    }

    internal async Task<HttpResponseMessage> SaveTodoItemAsync(
        string? title)
    {
        var addTodoItemRequest = new AddTodoItemRequest(
            Title: title);
        var request = new HttpRequestMessage(
            method: HttpMethod.Post,
            requestUri: "todoitems");

        request.Content = new StringContent(
            content: JsonConvert.SerializeObject(addTodoItemRequest),
            encoding: Encoding.UTF8,
            mediaType: "application/json");

        return await this.httpClient.SendAsync(request);
    }

    internal Task<HttpResponseMessage> UpdateTodoItemAsync(
        string id,
        string title,
        bool isCompleted)
    {
        var updateTodoItemRequest = new UpdateTodoItemRequest(
            Title: title,
            IsCompleted: isCompleted);

        var request = new HttpRequestMessage(
            method: HttpMethod.Put,
            requestUri: $"todoitems/{id}");

        request.Content = new StringContent(
            content: JsonConvert.SerializeObject(updateTodoItemRequest),
            encoding: Encoding.UTF8,
            mediaType: "application/json");

        return this.httpClient.SendAsync(request);
    }
}