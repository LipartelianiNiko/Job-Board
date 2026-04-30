using JobBoard.DTOs;
using JobBoard.DTOs.AuthDTOs;
using JobBoard.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        //inject AuthService
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/seeker")]
        public async Task<IActionResult> RegisterSeeker(SeekerRegisterDto dto)
        {
            try
            {
                //call the function for registration
                var result = await _authService.SeekerRegister(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //retunr 500 if couldnt register
                return StatusCode(500, new { message = ex.Message });
            }

        }

        [HttpPost("register/employer")]
        public async Task<IActionResult> RegisterEmployer(EmployerRegisterDto dto)
        {
            try
            {
                var result = await _authService.EmployerRegister(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _authService.Login(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }
    }
}
