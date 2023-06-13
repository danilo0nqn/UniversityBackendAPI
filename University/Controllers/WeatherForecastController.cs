using Microsoft.AspNetCore.Mvc;

namespace University.Controllers
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
        public IEnumerable<WeatherForecast> Get()
        {
            //add the logger to log when the methods get called.
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Trace Level Log");
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Debug Level Log");
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Information Level Log");
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Warning Level Log");
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Error Level Log");
            _logger.LogTrace($"{nameof(WeatherForecastController)} - {nameof(Get)} - Critical Level Log");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}