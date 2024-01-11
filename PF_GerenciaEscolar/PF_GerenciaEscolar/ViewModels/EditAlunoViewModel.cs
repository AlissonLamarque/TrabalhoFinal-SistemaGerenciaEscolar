using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.ViewModels
{
    public class EditAlunoViewModel
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Turma Turma { get; set; }
    }
}
