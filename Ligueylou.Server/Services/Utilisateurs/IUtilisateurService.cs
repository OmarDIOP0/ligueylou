using Ligueylou.Server.Dtos;
using Ligueylou.Server.Request;
using Ligueylou.Server.Response;
using Microsoft.AspNetCore.Mvc;

namespace Ligueylou.Server.Services.Utilisateurs
{
    public interface IUtilisateurService
    {
        Task<ActionResult<UserRegisterResponse>> Register(CreateUtilisateurDto createUtilisateurDto);
        Task<UtilisateurDto> GetUtilisateurById(Guid id);
        Task<UtilisateurDto> GetUtilisateurByEmail(string email);
        Task<IEnumerable<UtilisateurDto>> GetAllUtilisateurs();
        Task<UtilisateurDto> AddUtilisateur(CreateUtilisateurDto utilisateurDto);
    }
}
