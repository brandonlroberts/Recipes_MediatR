using Microsoft.EntityFrameworkCore;

namespace Recipes_MediatR.Components.Recipes.Tables.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Createddate { get; set; }
        public string? Createby { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<Instruction>? Instructions { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Measurements { get; set; }
    }

    public class Instruction
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string? Summary { get; set; }
    }

    class RecipeDb : DbContext
    {
        public RecipeDb(DbContextOptions options) : base(options) { }
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Instruction> Instructions { get; set; } = null!;
    }
}
