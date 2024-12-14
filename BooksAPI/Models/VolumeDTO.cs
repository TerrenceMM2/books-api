namespace BooksAPI.Models
{

  public class VolumeDTO
  {
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public IList<string> Authors { get; set; } = new List<string>();
    public string Publisher { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImageLink { get; set; } = "";
    public string InfoLink { get; set; } = "";
  }
}