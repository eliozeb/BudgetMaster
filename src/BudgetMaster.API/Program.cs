using BudgetMaster.Infrastructure.DependencyInjection;
using BudgetMaster.API.Middlewares;
using BudgetMaster.Infrastructure.Identity;
/* using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetMaster.Application.Interfaces;
using BudgetMaster.Infrastructure.Services; */
using BudgetMaster.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

//Configure Authentication Services:
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Access IConfiguration from the builder
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add ApplicationDbContext factory
builder.Services.AddDbContextFactory<ApplicationDbContext>((serviceProvider, options) =>
{
    // Optional: You can configure options here if needed
    // Since ApplicationDbContext.OnConfiguring handles the configuration,
    // you might leave this empty or configure logging if desired.

    // Example: Enable sensitive data logging (for development purposes only)
    options.EnableSensitiveDataLogging();

    // If you have any global configurations, you can set them here.
});

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

var key = builder.Configuration["JwtSettings:SecretKey"];
var issuer = builder.Configuration["JwtSettings:Issuer"];
var audience = builder.Configuration["JwtSettings:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});

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
// {jm
//     endpoints.MapControllers();
// });

app.Run();