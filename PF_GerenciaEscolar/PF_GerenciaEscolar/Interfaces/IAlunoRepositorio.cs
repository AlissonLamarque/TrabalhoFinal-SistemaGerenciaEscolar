using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Interfaces
{
    public interface IAlunoRepositorio
    {
        Task<IEnumerable<Aluno>> GetAll();

        Task<Aluno> GetByIdAsync(int id);

        bool Adicionar(Aluno aluno);
        bool Atualizar(Aluno aluno);
        bool Remover(Aluno aluno);
        bool Salvar();
    }
}
