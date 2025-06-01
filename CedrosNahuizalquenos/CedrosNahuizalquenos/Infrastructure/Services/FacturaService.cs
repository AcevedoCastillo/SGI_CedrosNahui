using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.Infrastructure.Data;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly ApplicationDbContext _context;

        public FacturaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas.FindAsync(id);
        }

        public async Task UpdateAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
            await _context.SaveChangesAsync();
        }
    }
}
