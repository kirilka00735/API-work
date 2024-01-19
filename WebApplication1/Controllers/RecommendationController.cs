using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Get()
        {
            List<Recommendation> recommendations = Context.Recommendations.ToList();
            return Ok(recommendations);
        }
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
        [HttpPost]
        public IActionResult Add(Recommendation recommendation)
        {
            Context.Recommendations.Add(recommendation);
            Context.SaveChanges();
            return Ok(recommendation);
        }
        [HttpPut]
        public IActionResult Update(Recommendation recommendation)
        {
            Context.Recommendations.Update(recommendation);
            Context.SaveChanges();
            return Ok(recommendation);
        }
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

