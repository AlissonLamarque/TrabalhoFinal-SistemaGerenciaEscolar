using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Interfaces
{
    public interface IAdministradorRepositorio
    {
        Task<IEnumerable<Administrador>> GetAll();

        Task<Administrador> GetByIdAsync(int id);

        bool Adicionar(Administrador admin);
        bool Atualizar(Administrador admin);
        bool Remover(Administrador admin);
        bool Salvar();
    }
}
