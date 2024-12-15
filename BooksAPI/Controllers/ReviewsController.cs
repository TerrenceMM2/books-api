using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace BooksAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly ReviewContext _context;

    public ReviewsController(ReviewContext context)
    {
      _context = context;
    }

    /// <summary>
    /// Returns all reviews for all books.
    /// </summary>
    /// <returns>A list of Reviews</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
      return await _context.Review.ToListAsync();
    }

    /// <summary>
    /// Returns review for a single book.
    /// </summary>
    /// <param name="volumeId"></param>
    /// <returns>A single Review</returns>
    [HttpGet("{volumeId}")]
    public async Task<ActionResult<List<Review>>> GetReview(string volumeId)
    {
      var reviews = new List<Review>();
      var reviewsResults = await _context.Review.Where(r => r.VolumeId == volumeId).ToListAsync();

      if (reviews == null)
      {
        return Ok("No results found");
      }

      foreach (var review in reviewsResults)
      {
        reviews.Add(review);
      }


      return reviews;
    }

    /// <summary>
    /// Adds new review for a single book.
    /// </summary>
    /// <param name="reviewRequest"></param>
    /// <returns>The newly created Review omitting the id.</returns>
    [HttpPost]
    [ActionName(nameof(GetReview))]
    public async Task<ActionResult<ReviewResponse>> PostReview([FromBody] ReviewRequest reviewRequest)
    {
      if (reviewRequest == null)
      {
        return BadRequest("Request cannot be null");
      }

      if (reviewRequest.ReviewText.IsNullOrEmpty() || reviewRequest.StarRating.CompareTo(0) == 0)
      {
        return BadRequest("ReviewText & StarRating cannot be null or empty");
      }

      var review = new Review
      {
        VolumeId = reviewRequest.VolumeId,
        ReviewText = reviewRequest.ReviewText,
        StarRating = reviewRequest.StarRating,
      };

      _context.Review.Add(review);
      await _context.SaveChangesAsync();

      var response = new ReviewResponse
      {
        VolumeId = review.VolumeId,
        ReviewText = review.ReviewText,
        StarRating = review.StarRating,
      };

      return CreatedAtAction(nameof(GetReview), new { id = review.Id }, response);
    }
  }
}
