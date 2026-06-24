using MedicineManager.App.Components;
using MedicineManager.App.Data.Models;
using MedicineManager.App.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register repositories.
// TODO: Replace JsonFileRepository with an EF Core implementation when introducing Entity Framework Core.
builder.Services.AddDbContext<MedicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepository<Medicine>, EfCoreRepository<Medicine>>();
builder.Services.AddScoped<IRepository<IntakeSchedule>, EfCoreRepository<IntakeSchedule>>();

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
