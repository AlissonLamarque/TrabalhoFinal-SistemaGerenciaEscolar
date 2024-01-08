using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Interfaces
{
    public interface INotaRepositorio
    {
        Task<IEnumerable<Nota>> GetAll();

        Task<Nota> GetByIdAsync(int id);

        bool Adicionar(Nota nota);
        bool Atualizar(Nota nota);
        bool Remover(Nota nota);
        bool Salvar();
    }
}
