using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models;

public class Review
{
  [Key]
  public int Id { get; set; }
  public required string VolumeId { get; set; }
  public required string ReviewText { get; set; }
  public int StarRating { get; set; }
}