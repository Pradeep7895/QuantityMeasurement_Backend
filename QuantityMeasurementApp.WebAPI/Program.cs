using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Repository.Implementations;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Services;
using StackExchange.Redis;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

// DB - reads from env var ConnectionStrings__DefaultConnection on Railway
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connStr))
    throw new InvalidOperationException("DefaultConnection is not configured. Set ConnectionStrings__DefaultConnection in Railway Variables.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connStr));

// Redis - optional, app still works without it
var redisConnection = builder.Configuration["Redis"];

if (!string.IsNullOrEmpty(redisConnection))
{
    try
    {
        var options = ConfigurationOptions.Parse(redisConnection);
        options.AbortOnConnectFail = false;

        var redis = ConnectionMultiplexer.Connect(options);
        builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
        builder.Services.AddScoped<RedisCacheService>();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Redis failed: " + ex.Message);
    }
}

// Services
builder.Services.AddScoped<IQuantityService, QuantityService>();
builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<IArithmeticService, ArithmeticService>();
builder.Services.AddScoped<IEqualityService, EqualityService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

// Repository
builder.Services.AddScoped<IQuantityHistoryRepository, QuantityHistoryRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT - throws a clear error if key is missing
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is not configured. Set Jwt__Key in Railway Variables.");

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// PORT - Railway injects this automatically
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

var app = builder.Build();


// Swagger always on
app.UseSwagger();
app.UseSwaggerUI();

// Correct middleware order - CORS must be before auth
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();

// REMOVED: app.UseHttpsRedirection() - Railway handles HTTPS at load balancer

app.MapControllers();
app.MapGet("/", () => "Quantity Measurement API is running...");

app.Run();
