using Ligueylou.Server.Dtos;

namespace Ligueylou.Server.Response
{
    public class UserRegisterResponse
    {
        public string? Token { get; set; }
        public DateTime TokenExpireAt { get; set; }
        public string? RefreshToken { get; set; }
        public UtilisateurDto? Utilisateur { get; set; }
    }
}
