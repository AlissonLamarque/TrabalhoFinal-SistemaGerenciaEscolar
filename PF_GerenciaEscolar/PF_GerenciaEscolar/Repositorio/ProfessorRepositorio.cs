using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Repositorio
{
    public class ProfessorRepositorio : IProfessorRepositorio
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public ProfessorRepositorio(PF_GerenciaEscolarDbContext contexto)
        {
            _contexto = contexto;
        }

        public bool Adicionar(Professor professor)
        {
            _contexto.Add(professor);
            return Salvar();
        }

        public bool Atualizar(Professor professor)
        {
            _contexto.Update(professor);
            return Salvar();
        }

        public async Task<IEnumerable<Professor>> GetAll()
        {
            return await _contexto.Professores.ToListAsync();
        }

        public async Task<Professor> GetByIdAsync(int id)
        {
            return await _contexto.Professores.Include(i => i.Autenticacao).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Remover(Professor professor)
        {
            _contexto.Remove(professor);
            return Salvar();
        }

        public bool Salvar()
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }
    }
}
