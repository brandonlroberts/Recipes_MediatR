using MediatR;
using System.Net.Http.Json;
using static Recipes_MediatR.Components.Recipes.Tables.Recipes.RecipesTableHandler;

namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public class RecipesTableHandler : IRequestHandler<GetRecipes, List<RecipesTableViewModel>>
    {
        private readonly HttpClient _httpClient;

        public RecipesTableHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class GetRecipes : IRequest<List<RecipesTableViewModel>>
        {
            public int PageSize { get; set; }

            public GetRecipes(int pageSize = 10)
            {
                PageSize = pageSize;
            }
        }

        public async Task<List<RecipesTableViewModel>> Handle(GetRecipes request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<List<RecipesTableViewModel>>("https://localhost:7164/recipes/");
            return response?.Take(request.PageSize).ToList()!;
        }
    }
}
