using Microsoft.EntityFrameworkCore;

namespace daebak_subdivision_website.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // User Management
        public DbSet<User> Users { get; set; }

        // Service Requests
        public DbSet<ServiceRequest> ServiceRequests { get; set; } // ✅ Fix: Ensure ServiceRequests exists

        // Facility & Reservations
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityReservation> FacilityReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FacilityReservation -> Facility Relationship
            modelBuilder.Entity<FacilityReservation>()
                .HasOne(fr => fr.Facility)
                .WithMany(f => f.FacilityReservations)
                .HasForeignKey(fr => fr.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);

            // FacilityReservation -> User Relationship
            modelBuilder.Entity<FacilityReservation>()
                .HasOne(fr => fr.User)
                .WithMany()
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

          
            modelBuilder.Entity<FacilityReservation>()
                .Property(fr => fr.Status)
                .HasAnnotation("CheckConstraint", "STATUS IN ('Pending', 'Approved', 'Denied')");
        }
    }
}
