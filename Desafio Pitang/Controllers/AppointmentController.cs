using DesafioPitang.Business.Interface.IBusinesses;
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
        private readonly IAppointmentBusiness _appointmentBusiness;
        public AppointmentController(IAppointmentBusiness appointmentBusiness)
        {
            _appointmentBusiness = appointmentBusiness;
        }
        [HttpGet("GetAll")]
        public async Task<List<AppointmentDTO>> GetAll()
        {
            return await _appointmentBusiness.GetAll();
        }

        [HttpGet("GetByDate/{date}")]
        public async Task<List<AppointmentDTO>> GetByDate(DateTime date)
        {
            return await _appointmentBusiness.GetByDate(date);
        }

        [HttpPatch("Update/Status")]
        [RequiredTransaction]
        public async Task<AppointmentDTO> UpdateStatus([FromBody] AppointmentStatusUpdateModel statusModel)
        {
            return await _appointmentBusiness.UpdateStatus(statusModel);
        }
    }
}