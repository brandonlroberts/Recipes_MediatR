using MediatR;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public record EditRecipeCoreTableData
    {
        public record Request(int RecipeId) : BlazorMediatRRequest, IRequest<Response>;

        public record Response : AuditableResponse
        {
            public List<RecipesTableViewModel> Recipes { get; set; } = new();
        }
    }

    public class EditRecipesTableHandler : IRequestHandler<EditRecipeCoreTableData.Request, EditRecipeCoreTableData.Response>
    {
        private readonly HttpClient _httpClient;

        public EditRecipesTableHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EditRecipeCoreTableData.Response> Handle(EditRecipeCoreTableData.Request request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<EditRecipeCoreTableData.Response>("https://localhost:7164/recipes/");
            return response;
        }
    }
}
