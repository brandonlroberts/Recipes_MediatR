using MediatR;
using Recipes_MediatR.Components.Recipes.Tables.Ingredients;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Pages.Recipes.Components.Tables.Insructions
{
    public record GetInstructionsFromRecipeTableData
    {
        public record Request(int RecipeId, int PageSize) : BlazorMediatRRequest, IRequest<Response>;

        public record Response : AuditableResponse
        {
            public List<InstructionViewModel> Instructions { get; set; } = new();
        }
    }

    public class GetInstructionsFromRecipeTableDataHandler : IRequestHandler<GetInstructionsFromRecipeTableData.Request, GetInstructionsFromRecipeTableData.Response>
    {
        private readonly HttpClient _httpClient;

        public GetInstructionsFromRecipeTableDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetInstructionsFromRecipeTableData.Response> Handle(GetInstructionsFromRecipeTableData.Request request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetFromJsonAsync<GetInstructionsFromRecipeTableData.Response>($"https://localhost:7164/recipe/{request.RecipeId}");
            response.Instructions.Take(request.PageSize).ToList();
            return response;
        }
    }
}
