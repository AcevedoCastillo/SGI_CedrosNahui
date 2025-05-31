using CedrosNahuizalquenos.Domain.Entities;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario?> GetByCredentialsAsync(string usuario, string contrasena);
    }
}
