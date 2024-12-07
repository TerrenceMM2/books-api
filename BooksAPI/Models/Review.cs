using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models;

public class Review
{
  [Key]
  public int Id { get; set; }
  public required string BookId { get; set; }
  public required string ReviewText { get; set; }
  public int StarRating { get; set; }
}