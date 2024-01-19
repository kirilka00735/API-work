using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

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
        [HttpGet]
        public IActionResult Get()
        {
            List<Place> places = Context.Places.ToList();
            return Ok(places);
        }
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
        [HttpPost]
        public IActionResult Add(Place place)
        {
            Context.Places.Add(place);
            Context.SaveChanges();
            return Ok(place);
        }
        [HttpPut]
        public IActionResult Update(Place place)
        {
            Context.Places.Update(place);
            Context.SaveChanges();
            return Ok(place);
        }
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

