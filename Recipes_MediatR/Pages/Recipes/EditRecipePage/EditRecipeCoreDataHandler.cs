using MediatR;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public record EditRecipeCoreData
    {
        public record Request(int Id, string Title, DateTime? CreatedDate, string CreatedBy) : BlazorMediatRRequest, IRequest;
    }

    public class EditRecipeCoreDataHandler : IRequestHandler<EditRecipeCoreData.Request>
    {
        private readonly HttpClient _httpClient;

        public EditRecipeCoreDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Handle(EditRecipeCoreData.Request request, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7164/recipe/{request.Id}", request);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"HTTP request failed with status code: {response.StatusCode}. Response Content: {responseContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
