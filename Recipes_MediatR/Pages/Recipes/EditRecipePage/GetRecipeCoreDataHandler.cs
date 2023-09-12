using MediatR;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public record GetRecipeCoreData
    {
        public record Request(int id) : BlazorMediatRRequest, IRequest<Response>;

        public record Response : AuditableResponse
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public DateTime? CreatedDate { get; set; }
            public string CreatedBy { get; set; } = string.Empty;
        }
    }

    public class GetRecipeCoreDataHandler : IRequestHandler<GetRecipeCoreData.Request, GetRecipeCoreData.Response>
    {
        private readonly HttpClient _httpClient;

        public GetRecipeCoreDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetRecipeCoreData.Response?> Handle(GetRecipeCoreData.Request request, CancellationToken cancellationToken)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GetRecipeCoreData.Response>($"https://localhost:7164/recipe/{request.id}");
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
