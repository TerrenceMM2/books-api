using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Google.Apis.Books.v1.Data;

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
    [HttpGet("{searchTerm}")]
    public async Task<ActionResult<List<VolumeDTO>>> GetBooks(IConfiguration configuration, string searchTerm)
    {
      if (string.IsNullOrEmpty(searchTerm))
      {
        return BadRequest();
      }

      Console.WriteLine($"Searching for ... {searchTerm}");

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
          var volume = CreateVolumeDTO(item);

          volumeList.Add(volume);
        }

        return volumeList;
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// Calls Google Books API
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="volumeId"></param>
    /// <returns>A single Volume result returned from Google Books API.</returns>
    [HttpGet("volume/{volumeId}")]
    public async Task<ActionResult<VolumeDTO>> GetBookById(IConfiguration configuration, string volumeId)
    {
      if (string.IsNullOrEmpty(volumeId))
      {
        return BadRequest();
      }

      Console.WriteLine($"Searching for ... {volumeId}");

      var service = new BooksService(new BaseClientService.Initializer
      {
        ApplicationName = "BooksAPI",
        ApiKey = configuration["GOOGLE_BOOKS_API:Key"]
      });

      var request = service.Volumes.Get(volumeId);

      Console.WriteLine("Executing Volumes Request ...");
      try
      {
        var result = await request.ExecuteAsync();
        Console.WriteLine("Volumes Request Complete ...");

        var volume = CreateVolumeDTO(result);

        return volume;
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    private static VolumeDTO CreateVolumeDTO(Volume volume)
    {
      if (volume == null) return new VolumeDTO();

      var volumeDTO = new VolumeDTO
      {
        Id = volume.Id,
        Title = volume.VolumeInfo?.Title ?? "",
        Authors = volume.VolumeInfo?.Authors ?? new string[0],
        Publisher = volume.VolumeInfo?.Publisher ?? "",
        Description = volume.VolumeInfo?.Description ?? "",
        InfoLink = volume.VolumeInfo?.InfoLink ?? "",
        ImageLink = volume.VolumeInfo?.ImageLinks?.Thumbnail ?? ""
      };

      return volumeDTO;
    }

  }
}
