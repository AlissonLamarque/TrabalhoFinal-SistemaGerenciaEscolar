using Microsoft.AspNetCore.Identity;
using PF_GerenciaEscolar.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_GerenciaEscolar.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public Disciplina Disciplina { get; set; }
    }
}
