using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.Review;
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
        /// <summary>
        /// Получить информацию о всех отзывах
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            List<Review> reviews = Context.Reviews.ToList();
            return Ok(reviews);
        }
        /// <summary>
        /// Получить информацию о отзыве по номеру ID
        /// </summary>
        // POST api/<ReviewController>
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
        /// <summary>
        /// Создание нового отзыва
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeId": 0,
        ///         "userId": 0,
        ///         "rating": 0.00,
        ///         "comment": "string",
        ///         "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="review">Пользователь</param>
        /// <returns></returns>

        // POST api/<ReviewController>
        [HttpPost]
        public IActionResult Add(CreateReviewRequest review)
        {
            var userDto = review.Adapt<Review>();
            Context.Reviews.Add(userDto);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Изменение данных об отзыве
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeId": 0,
        ///         "userId": 0,
        ///         "rating": 0.00,
        ///         "comment": "string",
        ///         "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="review">Пользователь</param>
        /// <returns></returns>

        // POST api/<ReviewController>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReviewUpdateRequest update)
        {
            var review = Context.Reviews.FirstOrDefault(r => r.ReviewId == id);
            if (review == null)
            {
                return NotFound("Review not found");
            }
            review = update.Adapt(review);
            Context.SaveChanges();
            return Ok("Review updated successfully");
        }
        /// <summary>
        /// Удаление отзыва
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Review? review = Context.Reviews.Where(r => r.ReviewId == id).FirstOrDefault();
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
