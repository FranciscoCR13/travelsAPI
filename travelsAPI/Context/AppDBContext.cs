using Microsoft.EntityFrameworkCore;

namespace travelsAPI.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){}
        public DbSet<Models.Place> Place { get; set; }
        public DbSet<Models.Operator> Operator { get; set; }

        public DbSet<Models.TravelStatus> TravelStatus { get; set; }
        public DbSet<Models.Travel> Travel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Travel>()
                .HasOne(t => t.Origin)
                .WithMany()
                .HasForeignKey(t => t.OriginId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Travel>()
                .HasOne(t => t.Destination)
                .WithMany()
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Travel>()
                .HasOne(t => t.Operator)
                .WithMany()
                .HasForeignKey(t => t.OperatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Travel>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
