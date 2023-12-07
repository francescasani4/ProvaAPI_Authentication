using ProvaAPI_Authentication.Database;
using Microsoft.EntityFrameworkCore;
using ProvaAPI_Authentication.Authentication;
using ProvaAPI_Authentication.Authentication.CustomAttributes;
using ProvaAPI_Authentication.Authentication.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProvaAPI_Authentication.Authentication.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// integrazione di Swagger con API Key
builder.Services.AddSwaggerGen(c=>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement
    {
        {key, new List<string>() }
    };

    c.AddSecurityRequirement(requirement);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

// builder.Services.AddScoped<ApiKeyAuthFilter>();

// si registra il servizio IHttpContextAccessor in modo da poter accedere a HttpContext
builder.Services.AddHttpContextAccessor();

/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiKeyPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
        policy.Requirements.Add(new ApiKeyRequirement());
    });
});

builder.Services.AddScoped<IAuthorizationHandler, ApiKeyHadler>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// registra il middleware personalizzato 
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

