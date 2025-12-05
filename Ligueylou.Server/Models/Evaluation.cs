using System.ComponentModel.DataAnnotations;

namespace Ligueylou.Server.Models
{
    public class Evaluation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Range(0, 5, ErrorMessage = "Le score doit être compris entre 0 et 5.")]
        public double Score { get; set; }
        public string? Commentaire { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
