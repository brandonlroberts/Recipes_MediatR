using MediatR;
using Recipes_MediatR.Components.Recipes.Tables.Ingredients;
using System.Net.Http.Json;
using static Recipes_MediatR.Components.Recipes.Tables.Recipes.IngredientTableHandler;

namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public class IngredientTableHandler : IRequestHandler<GetIngredients, List<IngredientViewModel>>
    {
        private readonly HttpClient _httpClient;

        public IngredientTableHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class GetIngredients : IRequest<List<IngredientViewModel>>
        {
            public int PageSize { get; set; }

            public GetIngredients(int pageSize = 10)
            {
                PageSize = pageSize;
            }
        }

        public async Task<List<IngredientViewModel>> Handle(GetIngredients request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<List<IngredientViewModel>>("https://localhost:7164/ingredients/");
            return response?.Take(request.PageSize).ToList()!;
        }
    }
}
