using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FinanceApp.Data;
using FinanceApp.Data.Service;

var builder = WebApplication.CreateBuilder(args);

// ✅ Säkerställ att konfigurationen är laddad
var configuration = builder.Configuration;

// Lägg till DbContext och använd anslutningssträngen från appsettings.json
builder.Services.AddDbContext<FinanceApp.Data.FinanceAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Lägg till din tjänst
builder.Services.AddScoped<IExpensesService, ExpensesService>();

// Lägg till Controllers-tjänster
builder.Services.AddControllersWithViews();  // ✅ Lägg till denna rad för att registrera controllers

// Lägg till Authorization (om du använder det)
builder.Services.AddAuthorization();

var app = builder.Build();

// Om miljön inte är utvecklingsläge, använd error handler
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Lägg till autentisering och auktorisering (om du använder det)
app.UseAuthentication(); // Lägg till om du använder autentisering
app.UseAuthorization();  // Lägg till om du använder auktorisering

// Definiera routen för controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}");



app.Run();
