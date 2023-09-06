namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public class RecipesTableViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Createddate { get; set; }
        public string? Createby { get; set; }
        public List<IngredientMeasurement>? IngredientMeasurements { get; set; }
    }
        
    public class IngredientMeasurement
    {
        public required Ingredient Ingredient { get; set; }
        public required Measurement Measurement { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Measurement
    {
        public int Id { get; set; }
        public int Grams { get; set; }
    }
}
