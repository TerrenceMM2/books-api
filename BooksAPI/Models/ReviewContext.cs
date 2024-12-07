using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;

namespace BooksAPI.Models;

public class ReviewContext : DbContext
{
  public ReviewContext(DbContextOptions<ReviewContext> options)
      : base(options)
  {
  }

  public DbSet<Review> Review { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Review>(entity =>
    {
      entity.HasKey(e => e.Id); // Set primary key
      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd(); // Indicates the ID is auto-incremented
    });

    base.OnModelCreating(modelBuilder);
  }
}