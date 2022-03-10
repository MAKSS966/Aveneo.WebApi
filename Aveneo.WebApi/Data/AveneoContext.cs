using Aveneo.WebApi.Model.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Data
{
    public class AveneoContext : DbContext
    {
        public AveneoContext(DbContextOptions<AveneoContext> options) : base(options)
        {

        }

        private void AddTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));


            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateDate = DateTime.Now;
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.Now;


                }
                else
                {
                    Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreateDate).IsModified = false;
                }

            }
        }

        public override int SaveChanges()
        {
            this.AddTimestamps();

            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
