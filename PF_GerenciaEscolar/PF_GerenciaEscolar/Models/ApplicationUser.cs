using Microsoft.AspNetCore.Identity;

namespace PF_GerenciaEscolar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? CPF { get; set; }
    }
}
