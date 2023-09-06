using Microsoft.EntityFrameworkCore;
using Recipes_MediatR.Components.Recipes.Tables.Recipes;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Recipes") ?? "Data Source=Recipes.db";
builder.Services.AddSqlite<RecipeDb>(connectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.MapGet("/recipes", async (RecipeDb db) =>
{
    var recipes = await db.Recipes.Include(x => x.Ingredients).Include(x => x.Instructions).ToListAsync();
    return recipes;
});

app.MapGet("/ingredients", async (RecipeDb db) => await db.Ingredients.ToListAsync());
app.MapGet("/instructions", async (RecipeDb db) => await db.Instructions.ToListAsync());

app.MapPost("/recipe", async (RecipeDb db, Recipe recipe) =>
{
    await db.Recipes.AddAsync(recipe);
    await db.SaveChangesAsync();
    return Results.Created($"/recipe/{recipe.Id}", recipe);
});

app.Run();
