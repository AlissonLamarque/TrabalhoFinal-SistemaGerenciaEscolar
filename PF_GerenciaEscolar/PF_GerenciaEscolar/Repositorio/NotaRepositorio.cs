using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Repositorio
{
    public class NotaRepositorio : INotaRepositorio
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public NotaRepositorio(PF_GerenciaEscolarDbContext contexto)
        {
            _contexto = contexto;
        }

        public bool Adicionar(Nota nota)
        {
            _contexto.Add(nota);
            return Salvar();
        }

        public bool Atualizar(Nota nota)
        {
            _contexto.Update(nota);
            return Salvar();
        }

        public async Task<IEnumerable<Nota>> GetAll()
        {
            return await _contexto.Notas.ToListAsync();
        }

        public async Task<Nota> GetByIdAsync(int id)
        {
            return await _contexto.Notas.FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Remover(Nota nota)
        {
            _contexto.Remove(nota);
            return Salvar();
        }

        public bool Salvar()
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }
    }
}
