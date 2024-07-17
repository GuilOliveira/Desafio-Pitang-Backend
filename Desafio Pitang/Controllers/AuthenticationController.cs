using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPitang.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;
        public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }
        [HttpGet("Login")]
        public async Task<UserTokenDTO> Login([FromBody]LoginModel login)
        {
            return await _authenticationBusiness.Login(login);
        }

        [HttpGet("RefreshToken")]
        [ProducesResponseType(typeof(UserTokenDTO), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<UserTokenDTO> RefreshToken()
        {
            return await _authenticationBusiness.RefreshToken();

        }
    }
}