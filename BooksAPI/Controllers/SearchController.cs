using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;

namespace BooksAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SearchController : ControllerBase
  {

    public SearchController() { }

    /// <summary>
    /// Calls Google Books API
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="searchTerm"></param>
    /// <returns>A list of Volumes returned from Google Books API.</returns>
    // GET: api/search
    [HttpGet("{searchTerm}")]
    public async Task<ActionResult<List<VolumeDTO>>> GetBooks(IConfiguration configuration, string searchTerm)
    {
      if (string.IsNullOrEmpty(searchTerm))
      {
        return BadRequest();
      }

      var service = new BooksService(new BaseClientService.Initializer
      {
        ApplicationName = "BooksAPI",
        ApiKey = configuration["GOOGLE_BOOKS_API:Key"]
      });

      var request = service.Volumes.List(searchTerm);
      request.MaxResults = 10;

      Console.WriteLine("Executing Volumes Request ...");
      try
      {
        var results = await request.ExecuteAsync();
        Console.WriteLine("Volumes Request Complete ...");
        var volumeList = new List<VolumeDTO>();

        foreach (var item in results.Items)
        {
          var volume = new VolumeDTO
          {
            Id = item.Id,
            Title = item.VolumeInfo.Title,
            Authors = item.VolumeInfo.Authors ?? [],
            Publisher = item.VolumeInfo.Publisher,
            Description = item.VolumeInfo.Description
          };
          volumeList.Add(volume);
        }

        return volumeList;
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}
