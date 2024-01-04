using PF_GerenciaEscolar.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PF_GerenciaEscolar.Models
{
    public class Avaliacao
    {
        [Key] public int Id { get; set; }
        public string? Titulo { get; set; }
        public decimal? Valor { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Prazo { get; set; }

        public Disciplina Disciplina { get; set; }
        public Turma Turma { get; set; }

        public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
