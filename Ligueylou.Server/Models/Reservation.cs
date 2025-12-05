using Ligueylou.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Libelle { get; set; }
        public string? Description { get; set; }
        public TypeServiceEnum TypeService { get; set; }
        public StatutEnum Statut { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DateModification { get; set; }
        public Guid? ClientId { get; set; }
        public virtual Client? Client { get; set; }
        public Guid? PrestataireId { get; set; }
        public virtual Prestataire? Prestataire { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
