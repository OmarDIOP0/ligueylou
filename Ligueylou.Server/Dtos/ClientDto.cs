using Ligueylou.Server.Models;

namespace Ligueylou.Server.Dtos
{
    public class ClientDto : UtilisateurDto
    {
        public Adresse? Adresse { get; set; }
        public int EvaluationsCount { get; set; }
        public int ReservationsCount { get; set; }
        public int FavorisCount { get; set; }
    }
}
