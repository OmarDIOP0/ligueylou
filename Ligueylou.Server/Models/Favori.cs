namespace Ligueylou.Server.Models
{
    public class Favori
    {
        public Guid PrestataireId { get; set; }
        public virtual Prestataire Prestataire { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}
