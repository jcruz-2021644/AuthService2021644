using AuthServiceIN6BM.Domain.Interface;
using AuthServiceIN6BM.Persistence.Data;
using AuthServiceIN6BM.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceIN6BM.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationService(this IserviceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(options => options.UserNpgsql(configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvetions());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddHealthChecks();

        return services;
    }
    public static IserviceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddEnpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }



}
