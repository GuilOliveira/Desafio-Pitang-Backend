using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<List<AppointmentDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetByDate/{date}")]
        public async Task<List<AppointmentDTO>> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("Update/Status")]
        [RequiredTransaction]
        public async Task<AppointmentDTO> UpdateStatus([FromBody] AppointmentStatusUpdateModel statusModel)
        {
            throw new NotImplementedException();
        }
    }
}