using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TaxRate> TaxRates { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}