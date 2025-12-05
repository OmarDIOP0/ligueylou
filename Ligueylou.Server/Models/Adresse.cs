using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Adresse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Pays { get; set; }

        public ICollection<Client> Clients { get; set; }
        public ICollection<Prestataire> Prestataires { get; set; }
    }
}
