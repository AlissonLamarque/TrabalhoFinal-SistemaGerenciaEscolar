using System.ComponentModel.DataAnnotations.Schema;

namespace PF_GerenciaEscolar.ViewModels
{
    public class LancarNotaViewModel
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int? AvaliacaoId { get; set; }
        public int? AlunoId { get; set; }
    }
}
