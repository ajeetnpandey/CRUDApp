using CRUDApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get connection string

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Use retrieved connection string

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Important: Add this for static files (CSS, JS, etc.)
app.UseRouting();
app.UseAuthorization();


// Apply migrations at startup (Improved)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // More robust migration strategy:
        context.Database.Migrate(); // Always try to migrate.  EF Core handles existing databases.
        Console.WriteLine("Migrations applied (or database already up-to-date).");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        // IMPORTANT: Log the exception properly in production!
        // Example using built-in logging:
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error applying migrations");

        throw; // Re-throw to prevent the app from starting on migration failure.
    }
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();