using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SchedulingController : ControllerBase
    {
        private readonly ISchedulingBusiness _schedulingBusiness;
        public SchedulingController(ISchedulingBusiness schedulingBusiness)
        {
            _schedulingBusiness = schedulingBusiness;
        }
        [HttpPost("Register")]
        [RequiredTransaction]
        [Authorize]
        public async Task<AppointmentDTO> Post([FromBody] SchedulingModel schedulingModel)
        {
            return await _schedulingBusiness.Post(schedulingModel);
        }
    }
}
