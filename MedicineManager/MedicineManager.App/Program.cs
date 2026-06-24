using MedicineManager.App.Components;
using MedicineManager.App.Data.Models;
using MedicineManager.App.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register repositories.
// TODO: Replace JsonFileRepository with an EF Core implementation when introducing Entity Framework Core.
builder.Services.AddScoped<IRepository<Medicine>, JsonFileRepository<Medicine>>();
builder.Services.AddScoped<IRepository<IntakeSchedule>, JsonFileRepository<IntakeSchedule>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
