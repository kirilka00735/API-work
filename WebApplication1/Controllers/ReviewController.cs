using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public ExoticPlacesContext Context { get; }
        public ReviewController(ExoticPlacesContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Review> reviews = Context.Reviews.ToList();
            return Ok(reviews);
        }
        [HttpGet("Id")]
        public IActionResult GetById(int id)
        {
            Review? review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (review == null)
            {
                return BadRequest("Not found");
            }
            return Ok(review);
        }
        [HttpPost]
        public IActionResult Add(Review review)
        {
            Context.Reviews.Add(review);
            Context.SaveChanges();
            return Ok(review);
        }
        [HttpPut]
        public IActionResult Update(Review review)
        {
            Context.Reviews.Update(review);
            Context.SaveChanges();
            return Ok(review);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Review? review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (review == null)
            {
                return BadRequest("Not found");
            }
            Context.Reviews.Remove(review);
            Context.SaveChanges();
            return Ok();
        }
    }
}
