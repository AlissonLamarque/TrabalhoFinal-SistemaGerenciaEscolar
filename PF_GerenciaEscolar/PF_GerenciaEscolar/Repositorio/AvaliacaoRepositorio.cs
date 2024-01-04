using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Repositorio
{
    public class AvaliacaoRepositorio : IAvaliacaoRepositorio
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public AvaliacaoRepositorio(PF_GerenciaEscolarDbContext contexto)
        {
            _contexto = contexto;
        }

        public bool Adicionar(Avaliacao avaliacao)
        {
            _contexto.Add(avaliacao);
            return Salvar();
        }

        public bool Atualizar(Avaliacao avaliacao)
        {
            _contexto.Update(avaliacao);
            return Salvar();
        }

        public async Task<IEnumerable<Avaliacao>> GetAll()
        {
            return await _contexto.Avaliacoes.ToListAsync();
        }

        public async Task<Avaliacao> GetByIdAsync(int id)
        {
            return await _contexto.Avaliacoes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Remover(Avaliacao avaliacao)
        {
            _contexto.Remove(avaliacao);
            return Salvar();
        }

        public bool Salvar()
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }
    }
}
