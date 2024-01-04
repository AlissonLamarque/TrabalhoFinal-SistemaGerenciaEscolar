using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public AlunoRepositorio(PF_GerenciaEscolarDbContext contexto)
        {
            _contexto = contexto;
        }

        public bool Adicionar(Aluno aluno)
        {
            _contexto.Add(aluno);
            return Salvar();
        }

        public bool Atualizar(Aluno aluno)
        {
            _contexto.Update(aluno);
            return Salvar();
        }

        public async Task<IEnumerable<Aluno>> GetAll()
        {
            return await _contexto.Alunos.ToListAsync();
        }

        public async Task<Aluno> GetByIdAsync(int id)
        {
            return await _contexto.Alunos.Include(i => i.Autenticacao).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Remover(Aluno aluno)
        {
            _contexto.Remove(aluno);
            return Salvar();
        }

        public bool Salvar()
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }
    }
}
