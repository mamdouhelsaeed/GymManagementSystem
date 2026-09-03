using GymManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Trainer 1 : M GymClass
            modelBuilder.Entity<GymClass>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.GymClasses)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Member M : M GymClass through Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Member)
                .WithMany(m => m.Enrollments)
                .HasForeignKey(e => e.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.GymClass)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.GymClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent a member from being enrolled twice in the same class
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.MemberId, e.GymClassId })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
