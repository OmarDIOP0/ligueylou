using Ligueylou.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Specialite
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Libelle { get; set; }
        public string ? Description { get; set; }
        public int AnneeExperience { get; set; }
        public TypeServiceEnum? Domaine { get; set; }
        public string? Certification { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public bool Actif { get; set; } = true;
        public HashSet<Prestataire> Prestataires { get; set; } = new HashSet<Prestataire>();
    }
}
