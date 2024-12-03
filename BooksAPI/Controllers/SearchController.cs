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

      Console.WriteLine("Building Volumes Request ...");
      var request = service.Volumes.List(searchTerm);
      request.MaxResults = 10;

      Console.WriteLine($"Volumes Request ... {request}");

      Console.WriteLine("Executing Volumes Request ...");
      var results = await request.ExecuteAsync();
      var volumeList = new List<VolumeDTO>();

      // Display the results
      foreach (var item in results.Items)
      {
        var volume = new VolumeDTO
        {
          Title = item.VolumeInfo.Title,
          Authors = item.VolumeInfo.Authors ?? [],
          Publisher = item.VolumeInfo.Publisher,
          Description = item.VolumeInfo.Description
        };
        volumeList.Add(volume);
      }

      return volumeList;

    }
  }
}
