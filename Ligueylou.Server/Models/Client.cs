using Ligueylou.Server.Models.abstracts;
using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Client : Utilisateur
    {
        public ICollection<Evaluation> Evaluations { get; set; } = new HashSet<Evaluation>();
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public ICollection<Favori> Favoris { get; set; } = new HashSet<Favori>();
    }
}
