using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuarioDtos = usuarios.Select(u => new UsuarioDTO
            {
                UsuarioId = u.UsuarioId,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Contrasena = u.Contrasena,
                Rol = u.Rol,
                Activo = u.Activo,
                FechaRegistro = u.FechaRegistro
            });
            return Ok(usuarioDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            var usuarioDto = new UsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Contrasena = usuario.Contrasena,
                Rol = usuario.Rol,
                Activo = usuario.Activo,
                FechaRegistro = usuario.FechaRegistro
            };
            return Ok(usuarioDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDTO dto)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Contrasena = dto.Contrasena,
                Rol = dto.Rol,
                Activo = dto.Activo,
                FechaRegistro = DateTime.Now
            };
            await _usuarioRepository.AddAsync(nuevoUsuario);
            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.UsuarioId }, nuevoUsuario);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDTO dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.Contrasena = dto.Contrasena;
            usuario.Rol = dto.Rol;
            usuario.Activo = dto.Activo;
            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
