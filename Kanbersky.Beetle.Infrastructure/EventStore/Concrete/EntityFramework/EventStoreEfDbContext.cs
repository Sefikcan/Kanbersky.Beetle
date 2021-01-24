using Kanbersky.Beetle.Infrastructure.EventSourcing.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Concrete.EntityFramework
{
    public class EventStoreEfDbContext : DbContext
    {
        public EventStoreEfDbContext(DbContextOptions<EventStoreEfDbContext> options) : base(options)
        {
            if (!Database.CanConnect())
                Database.EnsureCreated();
        }

        public DbSet<StoreMessage> StoreMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoreMessage>(b =>
            {
                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.CreatedDate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.AggregateId)
                    .IsRequired();

                b.Property(p => p.Type)
                    .IsRequired();

                b.Property(p => p.Data)
                    .IsRequired();

                b.HasIndex(k => new { k.AggregateId, k.Version });

                b.HasKey(k => k.Id);
            });
        }
    }
}
