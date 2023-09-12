namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public class RecipesTableViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
