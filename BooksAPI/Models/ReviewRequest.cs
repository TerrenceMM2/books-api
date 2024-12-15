namespace BooksAPI.Models;

public class ReviewRequest
{
  public required string VolumeId { get; set; }
  public required string ReviewText { get; set; }
  public int StarRating { get; set; }
}