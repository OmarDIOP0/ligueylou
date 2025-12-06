namespace Ligueylou.Server.Dtos
{
    public class ClientDto : UtilisateurDto
    {
        public int EvaluationsCount { get; set; }
        public int ReservationsCount { get; set; }
        public int FavorisCount { get; set; }
    }
}
