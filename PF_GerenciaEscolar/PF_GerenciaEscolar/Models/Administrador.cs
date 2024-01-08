using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_GerenciaEscolar.Models
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }
        public string? Email { get; set; }

        [ForeignKey("Autenticacao")]
        public int? AutenticacaoId { get; set; }
        public Autenticacao? Autenticacao { get; set; }
    }
}
