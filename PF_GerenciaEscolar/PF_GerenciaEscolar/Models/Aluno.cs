using PF_GerenciaEscolar.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_GerenciaEscolar.Models
{
    public class Aluno
    {
        [Key] public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }

        [ForeignKey("Autenticacao")]
        public string? AutenticacaoId { get; set; }
        public Autenticacao? Autenticacao { get; set; }

        public Turma Turma { get; set; }
        public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
