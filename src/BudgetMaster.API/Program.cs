using BudgetMaster.Infrastructure.DependencyInjection;
using BudgetMaster.API.Middleware;
using BudgetMaster.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetMaster.Application.Interfaces;
using BudgetMaster.Infrastructure.Services;
using BudgetMaster.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Access IConfiguration from the builder
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // IConfiguration is usually registered by default

// Register Infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

//Ensure that IConfiguration is available when constructing ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>();

// Register Application services if you have an extension method
//builder.Services.AddInfrastructureServices();

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Register Middleware in Startup
app.UseMiddleware<TenantMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });

app.Run();