namespace Recipes_MediatR.Pages.Recipes.EditRecipePage
{
    public class EditRecipeViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Createddate { get; set; }
        public string? Createby { get; set; }
    }
}
