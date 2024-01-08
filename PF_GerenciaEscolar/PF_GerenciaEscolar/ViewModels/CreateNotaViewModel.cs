using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_GerenciaEscolar.ViewModels
{
    public class CreateNotaViewModel
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int AvaliacaoId { get; set; }
        public int AlunoId { get; set; }
    }
}
