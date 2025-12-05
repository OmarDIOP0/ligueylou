using Ligueylou.Server.Models.abstracts;
using System.ComponentModel.DataAnnotations;
namespace Ligueylou.Server.Models
{
    public class Administrateur : Utilisateur
    {
        [Required]
        public string CodeSecret { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DerniereConnexion { get; set; }
    }
}
