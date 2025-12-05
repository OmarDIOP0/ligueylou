using Ligueylou.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Libelle { get; set; }
        public TypeNotificationEnum TypeNotification { get; set; }
        public string? Description { get; set; }    
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DateModification { get; set; }
        public Guid? ReservationId { get; set; }
        public bool Lu { get; set; } = false;
        public Guid? ServiceId { get; set; }
        public Guid? PaiementId { get; set; }
        public virtual Paiement? Paiement { get; set; }
        public virtual Service? Service { get; set; }
        public virtual Reservation? Reservation { get; set; }
    }
}
