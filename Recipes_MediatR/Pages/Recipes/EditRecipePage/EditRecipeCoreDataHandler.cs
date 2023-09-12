using MediatR;
using System.Net.Http.Json;
using static Recipes_MediatR.Pages.Recipes.EditRecipePage.EditRecipeCoreDataHandler;

namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public class EditRecipeCoreDataHandler : IRequestHandler<EditRecipeCoreData, EditRecipeViewModel>
    {
        private readonly HttpClient _httpClient;

        public EditRecipeCoreDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class EditRecipeCoreData : IRequest<EditRecipeViewModel>
        {
            public EditRecipeViewModel Recipe { get; set; }

            public EditRecipeCoreData(EditRecipeViewModel recipe)
            {
                Recipe = recipe;
            }
        }

        public async Task<EditRecipeViewModel> Handle(EditRecipeCoreData request, CancellationToken cancellationToken)
        {
            var updatedDate = new EditRecipeViewModel
            {
                Id = request.Recipe.Id,
                Createby = request.Recipe.Createby,
                Createddate = request.Recipe.Createddate,
                Title = request.Recipe.Title
            };
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7164/recipe/{request.Recipe.Id}", updatedDate);
            return request.Recipe;
        }
    }
}
