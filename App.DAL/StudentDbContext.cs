using App.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace App.DAL;

public partial class StudentDbContext : DbContext
{
    public StudentDbContext() { }
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("student");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.About)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.EnrollmentDate).HasColumnType("date");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}