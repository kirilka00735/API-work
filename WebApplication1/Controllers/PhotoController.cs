using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

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
        [HttpGet]
        public IActionResult Get()
        {
            List<Photo> photos = Context.Photos.ToList();
            return Ok(photos);
        }
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
        [HttpPost]
        public IActionResult Add(Photo photo)
        {
            Context.Photos.Add(photo);
            Context.SaveChanges();
            return Ok(photo);
        }
        [HttpPut]
        public IActionResult Update(Photo photo)
        {
            Context.Photos.Update(photo);
            Context.SaveChanges();
            return Ok(photo);
        }
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

