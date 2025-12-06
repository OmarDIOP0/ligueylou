using Ligueylou.Server.Models.Enums;

namespace Ligueylou.Server.Request
{
    public class CreateUtilisateurDto
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string? Email { get; set; }
        public SexeEnum? Sexe { get; set; }
        public string Password { get; set; }
        public string? Telephone { get; set; }
        public RoleEnum Role { get; set; }
    }
}
