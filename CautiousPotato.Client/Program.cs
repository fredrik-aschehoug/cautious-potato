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

builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IIngredientsService, IngredientsService>();

await builder.Build().RunAsync();
