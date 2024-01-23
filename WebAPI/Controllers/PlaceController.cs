using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.Photo;
using WebApplication1.Models;
using Mapster;
using WebApplication1.Contracts.Place;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        public ExoticPlacesContext Context { get; }
        public PlaceController(ExoticPlacesContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получить информацию о всех местах
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            List<Place> places = Context.Places.ToList();
            return Ok(places);
        }
        /// <summary>
        /// Получить информацию о месте по номеру ID
        /// </summary>
        [HttpGet("Id")]
        public IActionResult GetById(int id)
        {
            Place? place = Context.Places.Where(x => x.PlaceId == id).FirstOrDefault();
            if (place == null)
            {
                return BadRequest("Not found");
            }
            return Ok(place);
        }
        /// <summary>
        /// Создание нового места
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeName": "string",
        ///         "country": "string",
        ///         "description": "string",
        ///         "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="place">Пользователь</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Contracts.Place.CreatePlaceRequest place)
        {
            var userDto = place.Adapt<Place>();
            Context.Places.Add(userDto);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Изменение данных о месте
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeName": "string",
        ///         "country": "string",
        ///         "description": "string",
        ///         "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="place">Пользователь</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePlaceRequest update)
        {
            var place = Context.Places.FirstOrDefault(p => p.PlaceId == id);
            if (place == null)
            {
                return NotFound("Recommendation not found");
            }
            place = update.Adapt(place);
            Context.SaveChanges();
            return Ok("Recomendation updated successfully");
        }
        /// <summary>
        /// Удаление места
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Place? place = Context.Places.Where(x => x.PlaceId == id).FirstOrDefault();
            if (place == null)
            {
                return BadRequest("Not found");
            }
            Context.Places.Remove(place);
            Context.SaveChanges();
            return Ok();
        }
    }
}

