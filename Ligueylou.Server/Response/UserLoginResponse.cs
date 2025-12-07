namespace Ligueylou.Server.Response
{
    public class UserLoginResponse
    {
        public string? Token { get; set; }
        public DateTime TokenExpireAt { get; set; }
        public string? RefreshToken { get; set; }
        public UserReadDto? User { get; set; }
    }
}
