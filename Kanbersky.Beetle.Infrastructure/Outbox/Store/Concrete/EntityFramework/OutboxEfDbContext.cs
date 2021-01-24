using Kanbersky.Beetle.Infrastructure.Outbox.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kanbersky.Beetle.Infrastructure.Outbox.Store.Concrete.EntityFramework
{
    public class OutboxEfDbContext : DbContext
    {
        public OutboxEfDbContext(DbContextOptions<OutboxEfDbContext> options) : base(options)
        {
            if (!Database.CanConnect())
                Database.EnsureCreated();
        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OutboxMessage>(b =>
            {
                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.CreatedDate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.Type)
                    .IsRequired();

                b.Property(p => p.Data)
                    .IsRequired();

                b.HasKey(k => k.Id);
            });
        }
    }
}
