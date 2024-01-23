using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.Photo;
using WebApplication1.Models;
using Mapster;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        public ExoticPlacesContext Context { get; }
        public PhotoController(ExoticPlacesContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получить информацию о всех фото
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            List<Photo> photos = Context.Photos.ToList();
            return Ok(photos);
        }
        /// <summary>
        /// Получить информацию о фото по номеру ID
        /// </summary>
        [HttpGet("Id")]
        public IActionResult GetById(int id)
        {
            Photo? photo = Context.Photos.Where(x => x.PhotoId == id).FirstOrDefault();
            if (photo == null)
            {
                return BadRequest("Not found");
            }
            return Ok(photo);
        }
        /// <summary>
        /// Добавление нового фото
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeId": 0,
        ///         "userId": 0,
        ///         "photoUrl": "string",
        ///         "description": "string",
        ///         "createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="photo">Пользователь</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(CreatePhotoRequest photo)
        {
            var userDto = photo.Adapt<Photo>();
            Context.Photos.Add(userDto);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Изменение данных о фото
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///         "placeId": 0,
        ///"userId": 0,
        ///"photoUrl": "string",
        ///"description": "string",
        ///"createdBy": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="photo">Пользователь</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePhotoRequest update)
        {
            var photo = Context.Photos.FirstOrDefault(p => p.PhotoId == id);
            if (photo == null)
            {
                return NotFound("Photo not found");
            }
            photo = update.Adapt(photo);
            Context.SaveChanges();
            return Ok("Photo updated successfully");
        }
        /// <summary>
        /// Удаление фото
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Photo? photo = Context.Photos.Where(x => x.PhotoId == id).FirstOrDefault();
            if (photo == null)
            {
                return BadRequest("Not found");
            }
            Context.Photos.Remove(photo);
            Context.SaveChanges();
            return Ok();
        }
    }
}

