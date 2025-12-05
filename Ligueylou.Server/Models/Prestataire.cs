using Ligueylou.Server.Models.abstracts;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Prestataire : Utilisateur
    {
        [Range(0, 5, ErrorMessage = "Le score doit être compris entre 0 et 5.")]
        public double Score { get; set; }
        public Guid? AdresseId { get; set; }
        public virtual Adresse? Adresse { get; set; }
        public ICollection<Service> Services { get; set; } = new HashSet<Service>();
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public HashSet<Specialite> Specialites { get; set; } = new HashSet<Specialite>();
    }
}
