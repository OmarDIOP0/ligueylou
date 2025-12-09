using Ligueylou.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models.abstracts
{
    public abstract class Utilisateur
    {
        [Key]
        public Guid Id { get; set; }  = Guid.NewGuid();
        [Required, MinLength(3)]
        public string Prenom { get; set; }
        [Required, MinLength(1)]
        public string Nom { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public SexeEnum? Sexe { get; set; }
        [Required]
        public string Password { get; set; }
        [Phone]
        public string? Telephone { get; set; }
        public DateTime? DateDeNaissance { get; set; }
        public string? LieuDeNaissance { get; set; }
        public bool Actif { get; set; } 
        public RoleEnum Role { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
        public string? PhotoProfil { get; set; }
        public DateTime? DerniereConnexion { get; set; }
        public Guid? AdresseId { get; set; }
        public virtual Adresse? Adresse { get; set; }

    }
}
