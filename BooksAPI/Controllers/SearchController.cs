using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Google.Apis.Books.v1.Data;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace BooksAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SearchController : ControllerBase
  {

    public SearchController() { }

    // GET: api/search
    [HttpGet("{searchTerm}")]
    public async Task<ActionResult<VolumeDTO>> GetBooks(IConfiguration configuration, string searchTerm)
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

      Console.WriteLine("Building Volumes Request ...");
      var request = service.Volumes.List(searchTerm);
      request.MaxResults = 1;

      Console.WriteLine($"Volumes Request ... {request}");

      Console.WriteLine("Executing Volumes Request ...");
      var results = await request.ExecuteAsync();

      // Display the results
      foreach (var item in results.Items)
      {
        Console.WriteLine($"Title: {item.VolumeInfo.Title}");
        if (item.VolumeInfo.Authors != null)
        {
          Console.WriteLine($"Author(s): {string.Join(", ", item.VolumeInfo.Authors)}");
        }
        Console.WriteLine($"Publisher: {item.VolumeInfo.Publisher}");
        Console.WriteLine($"Description: {item.VolumeInfo.Description}");
        Console.WriteLine(new string('-', 50));
      }

      return Ok();

    }
  }
}
