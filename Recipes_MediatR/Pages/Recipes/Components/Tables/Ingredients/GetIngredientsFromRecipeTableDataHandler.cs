using MediatR;
using Recipes_MediatR.Components.Recipes.Tables.Ingredients;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Pages.Recipes.Components.Tables.Ingredients
{
    public record GetIngredientsFromRecipeTableData
    {
        public record Request(int RecipeId, int PageSize) : BlazorMediatRRequest, IRequest<Response>;

        public record Response : AuditableResponse
        {
            public List<IngredientViewModel> Ingredients { get; set; } = new();
        }
    }

    public class GetIngredientsFromRecipeTableDataHandler : IRequestHandler<GetIngredientsFromRecipeTableData.Request, GetIngredientsFromRecipeTableData.Response>
    {
        private readonly HttpClient _httpClient;

        public GetIngredientsFromRecipeTableDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetIngredientsFromRecipeTableData.Response> Handle(GetIngredientsFromRecipeTableData.Request request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<GetIngredientsFromRecipeTableData.Response>($"https://localhost:7164/recipe/{request.RecipeId}");
            response.Ingredients.Take(request.PageSize).ToList();
            return response;
        }
    }
}
