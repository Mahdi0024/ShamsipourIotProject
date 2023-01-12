using IotProject.Client;
using IotProject.Client.Services;
using IotProject.Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://de.gorazbang.ga") });

builder.Services.AddScoped<ITemperatureService, TemperatureService>();

await builder.Build().RunAsync();