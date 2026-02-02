using AuthServiceIN6BM.Persistence.Data;
using AuthServiceIN6BM.Api.Extensions;
using AuthServiceIN6BM.Api.ModelBinders;
using Serilog;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;


var builder = WebApplication.CreateBuilder(args);

builder.host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
.ReadFrom.configuration(context.Configuration)
.ReadFrom.SErvices(services));


builder.Services.AddControllers(options =>
{
    options.ModelBinderProvider.Insert(0, new FileDataBinderProvider());
})
.AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

builder.Services.AddApplicationService(builder.configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Add Serilog request logging
app.UseSerilogRequestLogging();

// Add Security Headers using NetEscapades package
app.UseSecurityHeaders(policies => policies
    .AddDefaultSecurityHeaders()
    .RemoveServerHeader()
    .AddFrameOptionsDeny()
    .AddXssProtectionBlock()
    .AddContentTypeOptionsNoSniff()
    .AddReferrerPolicyStrictOriginWhenCrossOrigin()
    .AddContentSecurityPolicy(builder =>
    {
        builder.AddDefaultSrc().Self();
        builder.AddScriptSrc().Self().UnsafeInline();
        builder.AddStyleSrc().Self().UnsafeInline();
        builder.AddImgSrc().Self().Data();
        builder.AddFontSrc().Self().Data();
        builder.AddConnectSrc().Self();
        builder.AddFrameAncestors().None();
        builder.AddBaseUri().Self();
        builder.AddFormAction().Self();
    })
    .AddCustomHeader("Permissions-Policy", "geolocation=(), microphone=(), camera=()")
    .AddCustomHeader("Cache-Control", "no-store, no-cache, must-revalidate, private")
);

//Global exception handling

// Core middLewares
app.UseHttpsRedirection();
app.UserCors("DefaultCorsPolicy");
app.UserRateLimiter();
app.UserAuthentication();
app.UserAuthorization();
app.MapControllers();
app.MapHelthChechks("/health");


app.MapGet("/health", () =>
{
    var response = new
    {
        status = "Healthy",
        timestamps = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
    };
    return Results.ok(response);
});

// Startup log: addresses and health endpoint
var startupLogger = app.Services.GetRequiredService<ILogger<Program>>();
app.Lifetime.ApplicationStarted.Register(() =>
{
    try
    {
        var server = app.Services.GetRequiredService<IServer>();
        var addressesFeature = server.Features.Get<IServerAddressesFeature>();
        var addresses = (IEnumerable<string>?)addressesFeature?.Addresses ?? app.Urls;

        if (addresses != null && addresses.Any())
        {
            foreach (var addr in addresses)
            {
                var health = $"{addr.TrimEnd('/')}/health";
                startupLogger.LogInformation("AuthService API is running at {Url}. Health endpoint: {HealthUrl}", addr, health);
            }
        }
        else
        {
            startupLogger.LogInformation("AuthService API started. Health endpoint: /health");
        }
    }
    catch (Exception ex)
    {
        startupLogger.LogWarning(ex, "Failed to determine the listening addresses for startup log");
    }
});

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Checking database connection...");

        // Ensure database is created (similar to Sequelize sync in Node.js)
        await context.Database.EnsureCreatedAsync();

        logger.LogInformation("Database ready. Running seed data...");
        await DataSeeder.SeedAsync(context);

        logger.LogInformation("Database initialization completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        throw; // Re-throw to stop the application
    }
}

app.Run();
