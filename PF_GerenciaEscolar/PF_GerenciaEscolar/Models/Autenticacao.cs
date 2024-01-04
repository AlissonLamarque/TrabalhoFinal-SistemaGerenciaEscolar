using Microsoft.AspNetCore.Identity;
using PF_GerenciaEscolar.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PF_GerenciaEscolar.Models
{
    public class Autenticacao : IdentityUser
    {
        public string? Cpf { get; set; }
        public string? Senha { get; set; }
        public Cargo Cargo { get; set; }

        public virtual ICollection<Administrador> Administradores { get; set; } = new List<Administrador>();
        public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
        public virtual ICollection<Professor> Professores { get; set; } = new List<Professor>();
    }
}
