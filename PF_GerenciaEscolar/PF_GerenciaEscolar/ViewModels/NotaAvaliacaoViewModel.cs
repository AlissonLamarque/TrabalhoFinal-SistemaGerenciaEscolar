using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.ViewModels
{
    public class NotaAvaliacaoViewModel
    {
        public class AvaliacaoViewModel
        {
            public Avaliacao? Avaliacao { get; set; }
            public List<NotaAlunoViewModel>? NotasAlunos { get; set; }
        }

        public class NotaAlunoViewModel
        {
            public Nota? Nota { get; set; }
            public Aluno? Aluno { get; set; }
        }
    }
}
