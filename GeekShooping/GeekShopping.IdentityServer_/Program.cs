//using GeekShopping.IdentityServer.Configuration;
//using GeekShopping.IdentityServer.Initializer;
//using GeekShopping.IdentityServer.Model.Context;
//using GeekShopping.ProductApi.Model.Context;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Configuração do Banco de Dados
//builder.Services.AddDbContext<MySQLContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Configuração do Identity
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<MySQLContext>()
//    .AddDefaultTokenProviders();

//// Configuração do IdentityServer
//var identityServerBuilder = builder.Services.AddIdentityServer(options =>
//{
//    options.Events.RaiseErrorEvents = true;
//    options.Events.RaiseInformationEvents = true;
//    options.Events.RaiseFailureEvents = true;
//    options.Events.RaiseSuccessEvents = true;
//    options.EmitStaticAudienceClaim = true;
//})
//.AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
//.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
//.AddInMemoryClients(IdentityConfiguration.Clients)
//.AddAspNetIdentity<ApplicationUser>()
//.AddDeveloperSigningCredential();

//builder.Services.AddScoped<IDbInitializer, DbInitializer>();

//// Add services to the container
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Resolva o inicializador do banco de dados
//using (var scope = app.Services.CreateScope())
//{
//    var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//    initializer.Initialize();
//}

//// Configure the HTTP request pipeline
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();
//app.UseIdentityServer();
//app.UseAuthorization();

//// Mapeamento de rotas
//app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Initializer;
using GeekShopping.IdentityServer.Model.Context;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseSqlServer(connectionString));

// Configuração do Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<MySQLContext>()
.AddDefaultTokenProviders();

// Configuração do IdentityServer
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
})
.AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
.AddInMemoryClients(IdentityConfiguration.Clients)
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer(); // Esta linha deve vir antes do UseAuthorization
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
