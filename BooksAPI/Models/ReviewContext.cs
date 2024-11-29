using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Models;

public class ReviewContext : DbContext
{
  public ReviewContext(DbContextOptions<ReviewContext> options)
      : base(options)
  {
  }

  public DbSet<ReviewItem> ReviewItems { get; set; } = null!;
}