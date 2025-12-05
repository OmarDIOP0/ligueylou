using Ligueylou.Server.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Paiement
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Montant { get; set; }
        public StatutEnum Statut { get; set; }
        public MethodePaiementEnum MethodePaiement { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public DateTime DatePaiement { get; set; } = DateTime.Now;
        public bool? Rembourse { get; set; } = false;
    }
}
