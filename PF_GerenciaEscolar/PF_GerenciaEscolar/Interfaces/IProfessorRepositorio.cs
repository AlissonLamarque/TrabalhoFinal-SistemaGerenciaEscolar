using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Interfaces
{
    public interface IProfessorRepositorio
    {
        Task<IEnumerable<Professor>> GetAll();

        Task<Professor> GetByIdAsync(int id);

        bool Adicionar(Professor professor);
        bool Atualizar(Professor professor);
        bool Remover(Professor professor);
        bool Salvar();
    }
}
