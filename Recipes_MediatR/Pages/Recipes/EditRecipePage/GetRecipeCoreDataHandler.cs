using MediatR;
using System.Net.Http.Json;
using static Recipes_MediatR.Pages.Recipes.EditRecipePage.GetRecipeCoreDataHandler;

namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public class GetRecipeCoreDataHandler : IRequestHandler<GetRecipeCoreData, EditRecipeViewModel>
    {
        private readonly HttpClient _httpClient;

        public GetRecipeCoreDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class GetRecipeCoreData : IRequest<EditRecipeViewModel>
        {
            public int Id { get; set; }

            public GetRecipeCoreData(int id)
            {
                Id = id;
            }
        }

        public async Task<EditRecipeViewModel> Handle(GetRecipeCoreData request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<EditRecipeViewModel>($"https://localhost:7164/recipe/{request.Id}");
            return response!;
        }
    }
}
