using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Interfaces
{
    public interface IAvaliacaoRepositorio
    {
        Task<IEnumerable<Avaliacao>> GetAll();

        Task<Avaliacao> GetByIdAsync(int id);
        Task<Avaliacao> GetByIdWithNotasAsync(int id);

        bool Adicionar(Avaliacao avaliacao);
        bool Atualizar(Avaliacao avaliacao);
        bool Remover(Avaliacao avaliacao);
        bool Salvar();
    }
}
