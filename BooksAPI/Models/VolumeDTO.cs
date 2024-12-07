namespace BooksAPI.Models
{

  public class VolumeDTO
  {
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required IList<string> Authors { get; set; }
    public required string Publisher { get; set; }
    public required string Description { get; set; }
  }
}