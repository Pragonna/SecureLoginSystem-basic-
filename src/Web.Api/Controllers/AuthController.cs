using Core.Infrastructure.Persistence.Users.Features.Commands.LoginCommand;
using Core.Infrastructure.Persistence.Users.Features.Commands.VerifyOTPCommand;
using Core.Infrastructure.Persistence.Users.Features.Commands.VerifyRefreshTokenCommand;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto login)
        {
            var command = new UserRegisterOrLoginCommand(login.Email, GetIpAddress());
            var result = await _sender.Send(command);
            return Ok(result);
        }
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromBody] OTPValidateCommand otpValidateCommand)
        {
            var result = await _sender.Send(otpValidateCommand);
            //Response.Cookies.Append("refresh-token", result.Response.AccessToken, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = result.Response.AccessTokenExpiryDate
            //});
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var command = new VerifyRefreshTokenCommand(refreshTokenDto.Email, refreshTokenDto.RefreshToken, GetIpAddress());
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
