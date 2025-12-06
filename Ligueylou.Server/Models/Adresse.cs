using Ligueylou.Server.Models.abstracts;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ligueylou.Server.Models
{
    public class Adresse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Rue { get; set; }
        public string Ville { get; set; }
        public string? CodePostal { get; set; }
        public string Pays { get; set; }

        public ICollection<Utilisateur> Utilisateurs { get; set; } = new HashSet<Utilisateur>();
    }
}
