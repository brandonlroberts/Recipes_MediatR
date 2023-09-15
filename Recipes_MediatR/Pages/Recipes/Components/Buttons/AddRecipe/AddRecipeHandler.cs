using FluentValidation;
using MediatR;
using Recipes_MediatR.Config;
using System.Net.Http.Json;

namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public class AddRecipeViewModelValidator : AbstractValidator<AddRecipeViewModel>
    {
        public AddRecipeViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
            RuleFor(x => x.CreatedDate)
                .NotEmpty();
            RuleFor(x => x.CreatedBy)
                .NotEmpty();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<AddRecipeViewModel>.CreateWithOptions((AddRecipeViewModel)model, x => x.IncludeProperties(propertyName)));
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }

    public record AddRecipeCoreData
    {
        public record Request(AddRecipeViewModel recipe) : BlazorMediatRRequest, IRequest;
        public record Response : AuditableResponse 
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string? CreatedBy { get; set; }
        }
    }

    public class AddRecipeCoreDataHandler : IRequestHandler<AddRecipeCoreData.Request>
    {
        private readonly HttpClient _httpClient;

        public AddRecipeCoreDataHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Handle(AddRecipeCoreData.Request request, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7164/recipe/", request.recipe);
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"HTTP request failed with status code: {response.StatusCode}. Response Content: {responseContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
