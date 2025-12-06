namespace Ligueylou.Server.Dtos
{
    public class PrestataireDto : UtilisateurDto
    {
        public double? Score { get; set; }
        public int ServicesCount { get; set; }
        public int ReservationsCount { get; set; }
        public int SpecialitesCount { get; set; }
        public int FavorisCount { get; set; }
    }
}
