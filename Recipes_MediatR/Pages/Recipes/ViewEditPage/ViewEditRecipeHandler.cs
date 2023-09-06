using MediatR;
using System.Net.Http.Json;
using static Recipes_MediatR.Pages.Recipes.ViewEditRecipeHandler;

namespace Recipes_MediatR.Pages.Recipes
{
    public class ViewEditRecipeHandler : IRequestHandler<GetRecipe, ViewEditRecieptViewModel>
    {
        private readonly HttpClient _httpClient;

        public ViewEditRecipeHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class GetRecipe : IRequest<ViewEditRecieptViewModel>
        {
            public int Id { get; set; }

            public GetRecipe(int id)
            {
                Id = id;
            }
        }

        public async Task<ViewEditRecieptViewModel> Handle(GetRecipe request, CancellationToken cancellationToken)
        {
            return await _httpClient.GetFromJsonAsync<ViewEditRecieptViewModel>($"https://localhost:7164/recipe/{request.Id}");
        }
    }
}
