using CauriousPotato.Requests.Ingredients;
using CautiousPotato.Components;
using CautiousPotato.Core.Models;
using CautiousPotato.Database;
using CautiousPotato.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddDatabase();

builder.Services.AddScoped<IIngredientService, IngredientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CautiousPotato.Client._Imports).Assembly);


var apiGroup = app.MapGroup("/api");
var ingredientsGroup = apiGroup .MapGroup("/ingredients");

ingredientsGroup.MapGet("/", ([FromServices] IIngredientService service) => service.GetAllAsync());
ingredientsGroup.MapGet("/{id:guid}", async ([FromServices] IIngredientService service, [FromRoute] Guid id) =>
    await service.GetAsync(new(id))
        is Ingredient ingredient
            ? Results.Ok(ingredient)
            : Results.NotFound());
ingredientsGroup.MapPost("/", async ([FromServices] IIngredientService service, [FromBody] CreateIngredientRequest request) =>
{
    var created = await service.CreateAsync(request);

    return Results.Created($"/{created.Id}", created);
});

app.Run();
