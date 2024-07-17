using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [HttpPost("Register")]
        [RequiredTransaction]
        public async Task<ActionResult> Post([FromBody] UserRegistrationModel registrationModel)
        {
            await _userBusiness.Register(registrationModel);
            return Ok();
        }
    }
}
