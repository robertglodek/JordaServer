using Hangfire;
using Jorda.Application.Common.Interfaces;
using Jorda.Infrastructure.Identity;
using Jorda.Infrastructure.Persistance;
using Jorda.Infrastructure.Persistance.Initializer;
using Jorda.Infrastructure.Persistance.Interceptors;
using Jorda.Infrastructure.Services;
using Jorda.Server.Application.Common.Configuration;
using Jorda.Server.Application.Common.Interfaces;
using Jorda.Server.Infrastructure.Identity;
using Jorda.Server.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jorda.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("SendGridSettings"));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<JordaUser, JordaRole>().AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDbInitialiser, DbContextInitialiser>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, SendGridEmailService>();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequiredLength = 8;
        });

        return services;
    }
}
