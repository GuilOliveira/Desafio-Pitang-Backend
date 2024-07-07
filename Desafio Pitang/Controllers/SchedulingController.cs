using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulingController : ControllerBase
    {
        [HttpPost("Register")]
        [RequiredTransaction]
        public async Task<AppointmentDTO> Post([FromBody] SchedulingModel schedulingModel)
        {
            throw new NotImplementedException();
        }
    }
}
