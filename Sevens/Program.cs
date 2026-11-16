using Sevens.Components;

var builder = WebApplication.CreateBuilder(args);
// to allow deploy on vercel
// comment out these two lines to test out locally
// Listen on the port Render provides (falls back to 8080 for local runs).
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();
// to allow deploy on vercel
// app.MapGet("/", () => Results.Json(new { message = "Hello from .NET on Vercel" }));
// app.MapGet("/health", () => Results.Json(new { status = "ok" }));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    .AddInteractiveServerRenderMode();

app.Run();
