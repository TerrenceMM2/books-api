using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
      return await _context.Review.ToListAsync();
    }

    // GET: api/reviews/5
    [HttpGet("{bookId}")]
    public async Task<ActionResult<Review>> GetReview(string bookId)
    {
      var review = await _context.Review.FirstOrDefaultAsync(r => r.BookId == bookId);

      if (review == null)
      {
        return NotFound();
      }

      return review;
    }

    // POST: api/reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(ReviewRequest reviewRequest)
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
        BookId = reviewRequest.BookId,
        ReviewText = reviewRequest.ReviewText,
        StarRating = reviewRequest.StarRating,
      };

      _context.Review.Add(review);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
    }

    // private static ReviewDTO ReviewToDTO(Review review) =>
    //   new ReviewDTO
    //   {
    //     Id = review.Id,
    //     BookId = review.BookId,
    //     StarRating = review.StarRating,
    //     ReviewText = review.ReviewText
    //   };
  }
}
