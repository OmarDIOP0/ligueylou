using Ligueylou.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace Ligueylou.Server.Models
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public TypeServiceEnum TypeService { get; set; }
        public int? Duree { get; set; }
        public double tarifHoraire { get; set; }
        public string? Description { get; set; }
        public Guid PrestataireId { get; set; }
        public virtual Prestataire Prestataire { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
        public Guid ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Paiement> Paiements { get; set; }
    }
}

