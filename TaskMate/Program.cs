using TaskMate.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DatabaseHelper>(sp =>
{
    // Pull IConfiguration that the host already loaded (appsettings.json, secrets, env vars)
    var cfg = sp.GetRequiredService<IConfiguration>();
    return new DatabaseHelper(cfg.GetConnectionString("DefaultConnection"));
});


builder.Services.AddControllersWithViews();
// Add services to the container for controllers (API)
builder.Services.AddControllers();  // No need for AddControllersWithViews if it's just API

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // Serve static files if needed
app.UseRouting();      // Enable routing




app.UseAuthorization();  // Optional, based on whether you're using authentication

// Default route for HomeController (optional if you're using MVC)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
