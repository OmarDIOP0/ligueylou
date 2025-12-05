using Ligueylou.Server.Models.abstracts;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Client : Utilisateur
    {
        public long? AdresseId { get; set; }
        public virtual Adresse? Adresse { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; } = new HashSet<Evaluation>();
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
