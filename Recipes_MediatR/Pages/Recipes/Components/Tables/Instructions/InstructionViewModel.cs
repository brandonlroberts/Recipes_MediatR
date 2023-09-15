namespace Recipes_MediatR.Components.Recipes.Tables.Ingredients
{
    public class InstructionViewModel
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Summary { get; set; } = string.Empty;
        public int Sequence { get; set; }
    }
}
