using Bank.Applications.Services;
using Bank.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto request)
        {
            var success = await _authService.RegisterClient(request.Name, request.Email, request.Password);
            if (!success) return BadRequest("Email déjà utilisé.");

            return Ok("Inscription réussie !");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateClient(request.Email, request.Password);
            if (token == null) return Unauthorized("Email ou mot de passe incorrect.");

            return Ok(new { Token = token });
        }
    }

}
