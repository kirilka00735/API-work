using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.Recommendation;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        public ExoticPlacesContext Context { get; }
        public RecommendationController(ExoticPlacesContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получить информацию о всех рекомендациях
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            List<Recommendation> recommendations = Context.Recommendations.ToList();
            return Ok(recommendations);
        }
        /// <summary>
        /// Получить информацию о рекомнедации по номеру ID
        /// </summary>
        // POST api/<RecommendationController>
        [HttpGet("Id")]
        public IActionResult GetById(int id)
        {
            Recommendation? recommendation = Context.Recommendations.Where(x => x.RecommendationId == id).FirstOrDefault();
            if (recommendation == null)
            {
                return BadRequest("Not found");
            }
            return Ok(recommendation);
        }
        /// <summary>
        /// Создание новой рекомендации
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeId": 0,
        ///         "userId": 0,
        ///         "comment": "string",
        ///         "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="recommendation">Пользователь</param>
        /// <returns></returns>

        // POST api/<RecommendationController>
        [HttpPost]
        public IActionResult Add(CreateRecomendation recommendation)
        {
            var userDto = recommendation.Adapt<Recommendation>();
            Context.Recommendations.Add(userDto);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Изменение данных о рекомендации
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///          "placeId": 0,
        ///          "userId": 0,
        ///          "comment": "string",
        ///          "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="recommendation">Пользователь</param>
        /// <returns></returns>

        // POST api/<RecommendationController>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReccomendationUpdate update)
        {
            var recommendation = Context.Recommendations.FirstOrDefault(r => r.RecommendationId == id);
            if (recommendation == null)
            {
                return NotFound("Recommendation not found");
            }
            recommendation = update.Adapt(recommendation);
            Context.SaveChanges();
            return Ok("Recomendation updated successfully");
        }
        /// <summary>
        /// Удаление рекомендации
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Recommendation? recommendation = Context.Recommendations.Where(x => x.RecommendationId == id).FirstOrDefault();
            if (recommendation == null)
            {
                return BadRequest("Not found");
            }
            Context.Recommendations.Remove(recommendation);
            Context.SaveChanges();
            return Ok();
        }
    }
}

