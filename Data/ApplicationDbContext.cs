using DRDOAssignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DRDOAssignment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerInfo> CustomerInfos { get; set; } = null!;
        public DbSet<Gender> Genders { get; set; } = null!;
        public DbSet<State> States { get; set; } = null!;
        public DbSet<District> Districts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Configure relationships
            builder.Entity<CustomerInfo>()
                .HasOne(c => c.Gender)
                .WithMany()
                .HasForeignKey(c => c.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CustomerInfo>()
                .HasOne(c => c.District)
                .WithMany()
                .HasForeignKey(c => c.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<District>()
                .HasOne(d => d.State)
                .WithMany(s => s.Districts)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Configure required fields
            builder.Entity<CustomerInfo>()
                .Property(c => c.Name)
                .IsRequired();

            builder.Entity<Gender>()
                .Property(g => g.Name)
                .IsRequired();

            builder.Entity<State>()
                .Property(s => s.Name)
                .IsRequired();

            builder.Entity<District>()
                .Property(d => d.Name)
                .IsRequired();
            #endregion
        }
    }
} 