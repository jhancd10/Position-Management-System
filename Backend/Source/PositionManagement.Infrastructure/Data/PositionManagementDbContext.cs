using Microsoft.EntityFrameworkCore;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Infrastructure.Data
{
    /// <summary>
    /// Represents the database context for the Position Management system.
    /// Provides access to the database tables and configuration for entity relationships.
    /// </summary>
    public class PositionManagementDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionManagementDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public PositionManagementDbContext(DbContextOptions<PositionManagementDbContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Gets or sets the Positions table in the database.
        /// </summary>
        public DbSet<Position> Positions { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Recruiters table in the database.
        /// </summary>
        public DbSet<Recruiter> Recruiters { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Departments table in the database.
        /// </summary>
        public DbSet<Department> Departments { get; set; } = null!;


        /// <summary>
        /// Configures the entity relationships and properties for the database context.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure the entity models.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Position entity
            modelBuilder.Entity<Position>(entity =>
            {
                // Set the primary key for the Position entity
                entity.HasKey(e => e.Id);

                // Configure the Title property as required with a maximum length of 100 characters
                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                // Configure the Description property as required with a maximum length of 1000 characters
                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasMaxLength(1000);

                // Configure the Location property as required with a maximum length of 200 characters
                entity.Property(e => e.Location)
                      .IsRequired()
                      .HasMaxLength(200);

                // Configure the Status property with a string conversion
                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasConversion<string>();

                // Configure the Budget property as required
                entity.Property(e => e.Budget)
                      .IsRequired();

                // Configure the relationship between Position and Recruiter with a foreign key
                entity.HasOne(e => e.Recruiter)
                      .WithMany(r => r.Positions)
                      .HasForeignKey(e => e.RecruiterId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configure the relationship between Position and Department with a foreign key
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Positions)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure the Recruiter entity
            modelBuilder.Entity<Recruiter>(entity =>
            {
                // Set the primary key for the Recruiter entity
                entity.HasKey(e => e.Id);

                // Configure the Name property as required with a maximum length of 100 characters
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                // Configure the Cellphone property as required with a maximum length of 20 characters
                entity.Property(e => e.Cellphone)
                      .IsRequired()
                      .HasMaxLength(15);

                // Configure the Email property as required with a maximum length of 100 characters
                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                // Configure the relationship between Recruiter and Positions
                entity.HasMany(e => e.Positions)
                      .WithOne(p => p.Recruiter)
                      .HasForeignKey(p => p.RecruiterId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure the Department entity
            modelBuilder.Entity<Department>(entity =>
            {
                // Set the primary key for the Department entity
                entity.HasKey(e => e.Id);

                // Configure the Name property as required with a maximum length of 100 characters
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                // Configure the relationship between Department and Positions
                entity.HasMany(e => e.Positions)
                      .WithOne(p => p.Department)
                      .HasForeignKey(p => p.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
