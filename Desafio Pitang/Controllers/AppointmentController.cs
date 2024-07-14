using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBusiness _appointmentBusiness;
        public AppointmentController(IAppointmentBusiness appointmentBusiness)
        {
            _appointmentBusiness = appointmentBusiness;
        }
        [HttpGet("GetAll")]
        public async Task<List<List<AppointmentDTO>>> GetAll()
        {
            return await _appointmentBusiness.GetAll();
        }

        [HttpGet("GetByDate")]
        public async Task<List<List<AppointmentDTO>>> GetByDate([FromQuery] DateTime initialDate, [FromQuery] DateTime finalDate)
        {
            return await _appointmentBusiness.GetByDate(initialDate, finalDate);
        }

        [HttpPatch("Update/Status")]
        [RequiredTransaction]
        public async Task<AppointmentDTO> UpdateStatus([FromBody] AppointmentStatusUpdateModel statusModel)
        {
            return await _appointmentBusiness.UpdateStatus(statusModel);
        }

        [HttpDelete("Delete")]
        [RequiredTransaction]
        public async Task<ActionResult> DeleteById([FromQuery] int id)
        {
            await _appointmentBusiness.DeleteById(id);
            return NoContent();
        }
    }
}