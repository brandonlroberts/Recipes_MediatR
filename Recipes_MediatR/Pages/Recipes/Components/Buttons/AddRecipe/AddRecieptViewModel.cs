namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public class AddRecipeViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
