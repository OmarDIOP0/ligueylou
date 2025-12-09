using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Favori
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Key]
        public Guid PrestataireId { get; set; }
        [Key]
        public Guid ClientId { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public virtual Prestataire Prestataire { get; set; }
        public virtual Client Client { get; set; }
        public bool Actif { get; set; } = true;
    }
}
