using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiniBlazorProject;
using MiniBlazorProject.Contracts;
using MiniBlazorProject.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var baseUrl = "https://nocodebackend-nocodebackend-stage.azurewebsites.net/api/v1/dataset/6428fa31cc6d53302309ceb8/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
builder.Services.AddScoped<IEnterpriseService, EnterpriseService>();
builder.Services.AddScoped<ISegmentService, SegmentService>();

await builder.Build().RunAsync();
