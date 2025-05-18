using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        public ProductosController(IProductoRepository repository)
        {
            _repository = repository;
        }

        // GET api/productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _repository.GetAllAsync();

            // Mapear manualmente a DTO
            var productosDto = productos.Select(p => new ProductoDTO
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                ImagenUrl = p.ImagenUrl,
                FechaRegistro = p.FechaRegistro,
                Activo = p.Activo
            }).ToList();

            return Ok(productosDto);
        }

        // POST api/productos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDTO dto)
        {
            var nuevoProducto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                ImagenUrl = dto.ImagenUrl,
                FechaRegistro = dto.FechaRegistro, // o dto.FechaRegistro si viene de frontend
                Activo = dto.Activo
            };

            await _repository.AddAsync(nuevoProducto);

            // Retornar el producto creado
            dto.ProductoId = nuevoProducto.ProductoId;
            dto.FechaRegistro = nuevoProducto.FechaRegistro;

            return CreatedAtAction(nameof(GetById), new { id = dto.ProductoId }, dto);
        }

        // GET api/productos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null) return NotFound();

            var dto = new ProductoDTO
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                ImagenUrl = producto.ImagenUrl,
                FechaRegistro = producto.FechaRegistro,
                Activo = producto.Activo
            };

            return Ok(dto);
        }

        // PUT api/productos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDTO dto)
        {
            if (id != dto.ProductoId)
                return BadRequest("ID mismatch");

            var productoExistente = await _repository.GetByIdAsync(id);
            if (productoExistente == null) return NotFound();

            // Actualizar campos
            productoExistente.Nombre = dto.Nombre;
            productoExistente.Descripcion = dto.Descripcion;
            productoExistente.Precio = dto.Precio;
            productoExistente.ImagenUrl = dto.ImagenUrl;
            productoExistente.Activo = dto.Activo;

            await _repository.UpdateAsync(productoExistente);

            return NoContent();
        }

        // DELETE api/productos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
