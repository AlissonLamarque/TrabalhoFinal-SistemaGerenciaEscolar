using PF_GerenciaEscolar.Data.Enum;

namespace PF_GerenciaEscolar.ViewModels
{
    public class CreateAvaliacaoViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Prazo { get; set; }
        public Disciplina Disciplina { get; set; }
        public Turma Turma { get; set; }
    }
}
