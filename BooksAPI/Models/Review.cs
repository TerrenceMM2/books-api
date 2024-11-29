namespace BooksAPI.Models;

public class Review
{
  public long Id { get; set; }
  public long ReviewId { get; set; }
  public string Text { get; set; } = "";
}