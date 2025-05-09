using Microsoft.EntityFrameworkCore;
using SubdivisionManagement.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddDbContext<HomeContext>(options =>
{
    options.UseSqlServer("Server=AMBL;Database=SubdivisionManagement_db;Trusted_Connection=True;TrustServerCertificate=True;");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "homeowner",
    pattern: "Homeowner/{action=Login}/{id?}",
    defaults: new { controller = "Homeowner", action = "Login" });

app.MapControllerRoute(
    name: "staff",
    pattern: "Staff/{action=Login}/{id?}",
    defaults: new { controller = "Staff", action = "Login" });

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=Login}/{id?}",
    defaults: new { controller = "Admin", action = "Login" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Homeowner}/{action=Dashboard}/{id?}");

SeedDatabase(app.Services);

app.Run();

void SeedDatabase(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<HomeContext>();
    
    try
    {
        // Ensure database is created
        context.Database.EnsureCreated();
        
        // Apply pending migrations
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        // Seed admin if none exists
        if (!context.Admins.Any())
        {
            context.Admins.Add(new Admin("admin", HashPassword("password123")));
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

static string HashPassword(string password)
{
    byte[] salt = Encoding.UTF8.GetBytes("password123");
    return Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: password,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 10000,
        numBytesRequested: 256 / 8));
}

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "text/plain"
});

