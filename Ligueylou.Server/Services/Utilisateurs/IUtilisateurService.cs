using Ligueylou.Server.Dtos;
using Ligueylou.Server.Request;

namespace Ligueylou.Server.Services.Utilisateurs
{
    public interface IUtilisateurService
    {
        Task<UtilisateurDto> GetUtilisateurById(Guid id);
        Task<UtilisateurDto> GetUtilisateurByEmail(string email);
        Task<IEnumerable<UtilisateurDto>> GetAllUtilisateurs();
        Task<UtilisateurDto> AddUtilisateur(CreateUtilisateurDto utilisateurDto);
    }
}
