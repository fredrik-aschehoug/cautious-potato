using CauriousPotato.Clients;
using CautiousPotato.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient();

builder.Services.AddHttpClient<IIngredientsClient, IngredientsClient>(client =>
{
    client.BaseAddress = new(builder.HostEnvironment.BaseAddress, UriKind.Absolute);
});
builder.Services.AddHttpClient<IRecipesClient, RecipesClient>(client =>
{
    client.BaseAddress = new(builder.HostEnvironment.BaseAddress, UriKind.Absolute);
});

builder.Services.AddFluentUIComponents();

builder.Services
    .AddScoped<IIngredientsService, IngredientsService>()
    .AddScoped<IRecipiesService, RecipiesService>();

await builder.Build().RunAsync();
