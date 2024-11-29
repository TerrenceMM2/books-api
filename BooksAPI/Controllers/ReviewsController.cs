using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Models;

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
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
    {
      return await _context.Reviews.Select(x => ReviewToDTO(x)).ToListAsync();
    }

    // GET: api/reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDTO>> GetReview(long id)
    {
      var review = await _context.Reviews.FindAsync(id);

      if (review == null)
      {
        return NotFound();
      }

      return ReviewToDTO(review);
    }

    // POST: api/reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
      _context.Reviews.Add(review);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
    }

    private static ReviewDTO ReviewToDTO(Review review) =>
      new ReviewDTO
      {
        Text = review.Text
      };
  }
}
