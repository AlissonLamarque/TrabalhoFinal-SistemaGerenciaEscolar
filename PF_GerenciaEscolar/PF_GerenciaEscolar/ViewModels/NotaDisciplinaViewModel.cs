using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.ViewModels
{
    public class NotaDisciplinaViewModel
    {
        public class AlunoViewModel
        {
            public Aluno? Aluno { get; set; }
            public List<NotaPorDisciplinaViewModel>? Notas { get; set; }
        }

        public class NotaPorDisciplinaViewModel
        {
            public Nota? Nota { get; set; }
            public Disciplina Disciplina { get; set; }
        }
    }
}
