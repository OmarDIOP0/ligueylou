namespace Ligueylou.Server.Dtos
{
    public class AdministrateurDto : UtilisateurDto
    {
        public string CodeSecret { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
