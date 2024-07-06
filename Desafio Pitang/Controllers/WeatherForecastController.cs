using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok("Ok");
        }
    }
}
