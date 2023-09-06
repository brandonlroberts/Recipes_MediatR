using Microsoft.EntityFrameworkCore;
using Recipes_MediatR.Components.Recipes.Tables.Recipes;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Recipes") ?? "Data Source=Recipes.db";
builder.Services.AddSqlite<RecipeDb>(connectionString);

// Add Cors
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Get Recipes
app.MapGet("/recipes", async (RecipeDb db) =>
{
    var recipes = await db.Recipes.Include(x => x.Ingredients).Include(x => x.Instructions).ToListAsync();
    return recipes;
});
// Get Recipe
app.MapGet("/recipe/{id}", async (int id, RecipeDb db) =>
{
    var recipe = await db.Recipes.Include(x => x.Ingredients).Include(x => x.Instructions).FirstAsync(x => x.Id == id);
    return recipe == null ? Results.NotFound() : Results.Ok(recipe);
});
// Create 
app.MapPost("/recipe", async (RecipeDb db, Recipe recipe) =>
{
    await db.Recipes.AddAsync(recipe);
    await db.SaveChangesAsync();
    return Results.Created($"/recipe/{recipe.Id}", recipe);
});
// Edit
app.MapPut("/recipe/{id}", async (int id, RecipeDb db, Recipe updatedRecipe) =>
{
    var existingRecipe = await db.Recipes.FirstOrDefaultAsync(x => x.Id == id);

    if (existingRecipe == null)
    {
        return Results.NotFound();
    }
    existingRecipe.Title = updatedRecipe.Title;
    existingRecipe.Createddate = updatedRecipe.Createddate;
    existingRecipe.Createby = updatedRecipe.Createby;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
// Delete
app.MapDelete("/recipe/{id}", async (int id, RecipeDb db) =>
{
    var recipe = await db.Recipes
        .Include(x => x.Ingredients)
        .Include(x => x.Instructions)
        .FirstOrDefaultAsync(x => x.Id == id);

    if (recipe == null)
    {
        return Results.NotFound();
    }

    if (recipe.Instructions != null) db.Instructions.RemoveRange(recipe.Instructions);
    if (recipe.Ingredients != null) db.Ingredients.RemoveRange(recipe.Ingredients);
    db.Recipes.Remove(recipe);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Get Ingredients
app.MapGet("/ingredients", async (RecipeDb db) => await db.Ingredients.ToListAsync());
// Get Ingredient
app.MapGet("/ingredient/{id}", async (int id, RecipeDb db) =>
{
    var ingredient = await db.Ingredients.FirstAsync(x => x.Id == id);
    return ingredient == null ? Results.NotFound() : Results.Ok(ingredient);
});
// Create 
app.MapPost("/ingredient", async (RecipeDb db, Ingredient ingredient) =>
{
    await db.Ingredients.AddAsync(ingredient);
    await db.SaveChangesAsync();
    return Results.Created($"/ingredient/{ingredient.Id}", ingredient);
});
// Edit
app.MapPut("/ingredient/{id}", async (int id, RecipeDb db, Ingredient updatedIngredient) =>
{
    var existingIngredient = await db.Ingredients.FirstOrDefaultAsync(x => x.Id == id);

    if (existingIngredient == null)
    {
        return Results.NotFound();
    }
    existingIngredient.Name = updatedIngredient.Name;
    existingIngredient.Measurements = updatedIngredient.Measurements;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
// Delete
app.MapDelete("/ingredient/{id}", async (int id, RecipeDb db) =>
{
    var ingredient = await db.Ingredients.FirstOrDefaultAsync(x => x.Id == id);

    if (ingredient == null)
    {
        return Results.NotFound();
    }
    db.Ingredients.Remove(ingredient);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Get Ingredients
app.MapGet("/instructions", async (RecipeDb db) => await db.Instructions.ToListAsync());
// Get Ingredient
app.MapGet("/instruction/{id}", async (int id, RecipeDb db) =>
{
    var instruction = await db.Instructions.FirstAsync(x => x.Id == id);
    return instruction == null ? Results.NotFound() : Results.Ok(instruction);
});
// Create 
app.MapPost("/instruction", async (RecipeDb db, Instruction instruction) =>
{
    await db.Instructions.AddAsync(instruction);
    await db.SaveChangesAsync();
    return Results.Created($"/instruction/{instruction.Id}", instruction);
});
// Edit
app.MapPut("/instruction/{id}", async (int id, RecipeDb db, Instruction updatedInstruction) =>
{
    var existingInstruction = await db.Instructions.FirstOrDefaultAsync(x => x.Id == id);

    if (existingInstruction == null)
    {
        return Results.NotFound();
    }
    existingInstruction.Sequence = updatedInstruction.Sequence;
    existingInstruction.Summary = updatedInstruction.Summary;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
// Delete
app.MapDelete("/instruction/{id}", async (int id, RecipeDb db) =>
{
    var instruction = await db.Instructions.FirstOrDefaultAsync(x => x.Id == id);

    if (instruction == null)
    {
        return Results.NotFound();
    }
    db.Instructions.Remove(instruction);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
