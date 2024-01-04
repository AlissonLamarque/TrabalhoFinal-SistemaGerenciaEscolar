using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.Repositorio
{
    public class AdministradorRepositorio : IAdministradorRepositorio
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public AdministradorRepositorio(PF_GerenciaEscolarDbContext contexto)
        {
            _contexto = contexto;
        }

        public bool Adicionar(Administrador admin)
        {
            _contexto.Add(admin);
            return Salvar();
        }

        public bool Atualizar(Administrador admin)
        {
            _contexto.Update(admin);
            return Salvar();
        }

        public async Task<IEnumerable<Administrador>> GetAll()
        {
            return await _contexto.Administradores.ToListAsync();
        }

        public async Task<Administrador> GetByIdAsync(int id)
        {
            return await _contexto.Administradores.Include(i => i.Autenticacao).FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Remover(Administrador admin)
        {
            _contexto.Remove(admin);
            return Salvar();
        }

        public bool Salvar()
        {
            var salvo = _contexto.SaveChanges();
            return salvo > 0 ? true : false;
        }
    }
}
