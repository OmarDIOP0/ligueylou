using Ligueylou.Server.Models.Enums;

namespace Ligueylou.Server.Dtos
{
    public class UtilisateurDto
    {
        public Guid Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string? Email { get; set; }
        public SexeEnum? Sexe { get; set; }
        public string? Telephone { get; set; }
        public RoleEnum Role { get; set; }
        public bool Actif { get; set; }
        public DateTime DateCreation { get; set; }

    }
}
