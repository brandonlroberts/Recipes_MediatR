using MediatR;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public record GetRecipeCoreTableData
    {
        public record Request(int PageSize) : BlazorMediatRRequest, IRequest<Response>;

        public record Response : AuditableResponse
        {
            public List<RecipesTableViewModel> Recipes { get; set; }
        }
    }

    public class RecipesTableHandler : IRequestHandler<GetRecipeCoreTableData.Request, GetRecipeCoreTableData.Response>
    {
        private readonly HttpClient _httpClient;

        public RecipesTableHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetRecipeCoreTableData.Response> Handle(GetRecipeCoreTableData.Request request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<GetRecipeCoreTableData.Response>("https://localhost:7164/recipes/");
            response.Recipes = response.Recipes.Take(request.PageSize).ToList();
            return response;
        }
    }
}
