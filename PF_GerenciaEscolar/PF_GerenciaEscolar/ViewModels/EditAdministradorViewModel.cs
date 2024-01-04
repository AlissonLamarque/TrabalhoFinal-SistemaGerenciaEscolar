using PF_GerenciaEscolar.Models;

namespace PF_GerenciaEscolar.ViewModels
{
    public class EditAdministradorViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? AutenticacaoId { get; set; }
        public Autenticacao Autenticacao { get; set; }
    }
}
