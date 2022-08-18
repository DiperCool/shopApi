using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IHashPassword _hashPassword;
    private readonly IBillingService _billingService;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, IHashPassword hashPassword, IBillingService billingService)
    {
        _logger = logger;
        _context = context;
        _hashPassword = hashPassword;
        _billingService = billingService;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if(_context.Database.GetPendingMigrations().Any())
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
        // Default users
        var administrator = new User { FirstName="Oleg", LastName="Olegovich", Email = "psdiper@gmail.com", Password=_hashPassword.Hash("1234"), Role=Domain.Enums.Role.Admin, Phone="380730111111" };

        if (! (await _context.Users.AnyAsync(u => u.Email == administrator.Email)))
        {
            string billingId = _billingService.CreateBillingUser(administrator.Email);
            administrator.BillingId=billingId;
            await _context.Users.AddAsync(administrator);
            await _context.SaveChangesAsync();

        }

        // Default data
        // Seed, if necessary
        if (!_context.Products.Any())
        {
            _context.Products.Add(new Product
            {
                Title = "Phone",
                Description = "This phone can call",
                Price = 100
                
            });
            await _context.SaveChangesAsync();

        }
    }
}
