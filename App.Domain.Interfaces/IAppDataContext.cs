using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

// NOTE: This depends on the Entity Framework Core

namespace App.Domain.Interfaces
{
    public interface IAppDataContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Review> Reviews { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}