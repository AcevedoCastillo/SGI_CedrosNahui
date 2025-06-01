using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using System;
using static CedrosNahuizalquenos.Client.Pages.Login;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{


    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioRepository.GetByCredentialsAsync(request.Usuario, request.Contrasena);

            if (usuario != null)
            {
                string token = "token_simulado";
                int rolId = usuario.Rol;               
                return Ok(new LoginResponse { Token = token, RolId = rolId });
            }

            return Unauthorized("Credenciales inválidas");
        }

        public class LoginRequest
        {
            public string Usuario { get; set; }
            public string Contrasena { get; set; }
        }
        public class LoginResponse
        {
            public string Token { get; set; }
            public int RolId { get; set; } // 1 = Admin, 2 = Empleado
        }
    }

}