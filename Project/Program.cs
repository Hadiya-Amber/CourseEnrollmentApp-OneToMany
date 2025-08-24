using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restuarant_Management.Data;
using Restuarant_Management.Interfaces;
using Restuarant_Management.Models;
using Restuarant_Management.Repository;
using Restuarant_Management.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// 1️⃣ Database Context (Scoped by default)
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));


// 2️⃣ Dependency Injection (DI) Registrations

//Repositories (Scoped → new per request)
builder.Services.AddScoped<INamedEntityRepository<Restaurant>, RestaurantRepository>();
builder.Services.AddScoped<INamedEntityRepository<Chef>, ChefRepository>();
builder.Services.AddScoped<INamedEntityRepository<Cuisine>, CuisineRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

//Services (Scoped → new per request)
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<ChefService>();
builder.Services.AddScoped<CuisineService>();
builder.Services.AddScoped<UserService>();

//  Token service (Singleton → one instance for app lifetime)
builder.Services.AddSingleton<IToken, TokenService>();

// 3️⃣ Controllers + JSON Options
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// Authorization
builder.Services.AddAuthorization();

//  Swagger + JWT Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Restaurant Management API",
        Version = "v1",
        Description = "API for managing restaurants, chefs, cuisines & users with JWT authentication"
    });

    //JWT Support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {token}' (without quotes). Example: Bearer eyJhbGciOiJIUzI1...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// DI lifetimes demo
builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();

// Middleware Pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
