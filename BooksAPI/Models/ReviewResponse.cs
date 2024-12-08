namespace BooksAPI.Models;

public class ReviewResponse
{
  public required string BookId { get; set; }
  public required string ReviewText { get; set; }
  public int StarRating { get; set; }
}