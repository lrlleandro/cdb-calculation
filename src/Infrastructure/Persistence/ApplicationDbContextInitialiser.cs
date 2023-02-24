using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Persistence
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly IApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
            if (!_context.TaxRates.Any())
            {
                var taxRates = new TaxRate[] 
                {
                    new TaxRate { Rate = 0.225, LimitInMonths = 6 },
                    new TaxRate { Rate = 0.2, LimitInMonths = 12 },
                    new TaxRate { Rate = 0.175, LimitInMonths = 24 },
                    new TaxRate { Rate = 0.15, LimitInMonths = int.MaxValue }
                };

                _context.TaxRates.AddRange(taxRates);
                await _context.SaveChangesAsync();
            }
        }
    }
}