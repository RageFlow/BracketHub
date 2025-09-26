using BracketHubWeb;
using BracketHubWeb.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<GameService>();

builder.Services.AddHttpClient<APIClient>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(5);
#if DEBUG
    client.BaseAddress = new Uri("http://localhost:5017");
#else
    //client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    client.BaseAddress = new Uri("https://gregerdesign.dk");
#endif
});

await builder.Build().RunAsync();
