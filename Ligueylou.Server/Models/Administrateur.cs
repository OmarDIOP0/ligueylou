using Ligueylou.Server.Models.abstracts;
namespace Ligueylou.Server.Models
{
    public class Administrateur : Utilisateur
    {
        public string CodeSecret { get; set; }
    }
}
