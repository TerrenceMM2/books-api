using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PingController : ControllerBase
  {
    /// <summary>
    /// Returns all reviews for all books.
    /// </summary>
    /// <returns>A list of Reviews</returns>
    [HttpGet]
    [Route("/ping")] // Explicitly sets the route to `/ping`
    [ApiExplorerSettings(IgnoreApi = true)] // Excludes from Swagger
    public IActionResult GetPing()
    {
      Console.WriteLine("Ping request received ...");
      return Ok("All good");
    }
  }
}
