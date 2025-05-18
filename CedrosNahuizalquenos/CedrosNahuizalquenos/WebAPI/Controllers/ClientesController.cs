using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cliente = await _clienteRepository.GetAllAsync();
            var clienteDtos = cliente.Select(c => new ClienteDTO
            { 
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Telefono = c.Telefono,
                Correo = c.Correo,
                Direccion = c.Direccion,
                FechaRegistro = c.FechaRegistro
            });
            return Ok(clienteDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            var clienteDtos =  new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo,
                Direccion = cliente.Direccion,
                FechaRegistro = cliente.FechaRegistro
            };
            return Ok(clienteDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO dto)
        {
            var nuevoCliente = new Cliente
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Direccion = dto.Direccion,
                FechaRegistro = dto.FechaRegistro
            };

            await _clienteRepository.AddAsync(nuevoCliente);

            return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.ClienteId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDTO dto)
        {
            if (id != dto.ClienteId)
                return BadRequest("ID mismatch");

            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            cliente.Nombre = dto.Nombre;
            cliente.Telefono = dto.Telefono;
            cliente.Correo = dto.Correo;
            cliente.Direccion = dto.Direccion;
            cliente.FechaRegistro = dto.FechaRegistro;

            await _clienteRepository.UpdateAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
