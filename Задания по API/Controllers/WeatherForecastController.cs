using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get(int? index)
        {
            if (index.HasValue && index < 0)
            {
                return BadRequest("Index cannot be negative.");
            }

            var weatherForecasts = Enumerable.Range(1, 5).Select(i => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(i),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(weatherForecasts);
        }


        [HttpGet("index", Name = "GetWeatherByIndex")]
        public ActionResult<string> GetWeatherByIndex(int index)
        {
            if (index < 0)
            {
                return BadRequest("Index cannot be negative.");
            }

            if (index >= Summaries.Length)
            {
                return NotFound("Index is out of range.");
            }

            return Summaries[index];
        }

        [HttpGet("find-by-name", Name = "GetWeatherCountByName")]
        public int GetWeatherCountByName(string name)
        {
            int count = Summaries.Count(summary => summary.Equals(name, StringComparison.OrdinalIgnoreCase));
            return count;
        }

        [HttpGet("GetAll", Name = "GetAllWeather")]
        public IActionResult GetAll(int? sortStrategy)
        {
            var allRecords = Enumerable.Range(1, 5).Select(i => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(i),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();

            if (sortStrategy == null)
            {
                return Ok(allRecords);
            }
            else if (sortStrategy == 1)
            {
                return Ok(allRecords.OrderBy(record => record.Summary).ToArray());
            }
            else if (sortStrategy == -1)
            {
                return Ok(allRecords.OrderByDescending(record => record.Summary).ToArray());
            }
            else
            {
                return BadRequest("Некорректное значение параметра sortStrategy");
            }
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}
