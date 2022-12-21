using Jorda.Application.Common.Constants;
using Jorda.Server.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jorda.Infrastructure.Persistance.Initializer;

public class DbContextInitialiser : IDbInitialiser
{
    private readonly ILogger<DbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<JordaUser> _userManager;
    private readonly RoleManager<JordaRole> _roleManager;

    public DbContextInitialiser(ILogger<DbContextInitialiser> logger, ApplicationDbContext context, UserManager<JordaUser> userManager,
        RoleManager<JordaRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task InitialiseAsync()
    {
        try
        {
            if (await _context.Database.CanConnectAsync())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
    public async Task TrySeedAsync()
    {

        if (await _context.Database.CanConnectAsync())
        {
            var administratorRole = new JordaRole(UserRolesConstants.RoleAdmin);
            var ultimateUserRole = new JordaRole(UserRolesConstants.RoleUltimate);
            var basicUserRole = new JordaRole(UserRolesConstants.RoleBasic);
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(administratorRole);
                await _roleManager.CreateAsync(ultimateUserRole);
                await _roleManager.CreateAsync(basicUserRole);
            }
        }
       
    }
}
