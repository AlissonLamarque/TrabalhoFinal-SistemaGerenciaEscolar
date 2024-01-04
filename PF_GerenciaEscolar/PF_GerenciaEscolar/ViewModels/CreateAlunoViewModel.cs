using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.ViewModels
{
    public class CreateAlunoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Autenticacao Autenticacao { get; set; }
        public Turma Turma { get; set; }
    }
}
