using CedrosNahuizalquenos.Domain.Entities;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IFacturaService
    {
        Task<Factura?> GetByIdAsync(int id);
        Task UpdateAsync(Factura factura);
    }
}
