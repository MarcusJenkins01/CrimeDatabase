using CrimeDatabase.Application.DependencyInjection;
using CrimeDatabase.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add MVC Controllers + Views
builder.Services.AddControllersWithViews();

// Register Infrastructure Layer (DbContext and the repositories).
// The DefaultConnection string must be set via dotnet user-secrets set
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")!);

// Register Application layer (the Crime and NotesAudit services).
builder.Services.AddApplication();

var app = builder.Build();

// Configure middleware pipeline
app.UseStaticFiles();
app.UseRouting();

// Map the Crimes index page as the default route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Crimes}/{action=Index}/{id?}"
);

// Map the /audits route to the NotesAudits/Index.cshtml page.
app.MapControllerRoute(
    name: "audits",
    pattern: "audits/{action=Index}/{id?}",
    defaults: new { controller = "NotesAudits" }
);

app.Run();
