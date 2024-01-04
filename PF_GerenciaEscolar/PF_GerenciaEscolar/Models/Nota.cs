using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_GerenciaEscolar.Models
{
    public class Nota
    {
        [Key] public int Id { get; set; }
        public decimal Valor { get; set; }
        [ForeignKey("Avaliacao")]
        public int? AvaliacaoId { get; set; }
        [ForeignKey("Aluno")]
        public int? AlunoId { get; set; }
    }
}
