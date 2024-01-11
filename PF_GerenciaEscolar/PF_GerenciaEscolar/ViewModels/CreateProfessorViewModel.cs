using Microsoft.AspNetCore.Identity;
using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.ViewModels
{
    public class CreateProfessorViewModel
    {
        public ApplicationUser User { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
