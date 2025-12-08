using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Request;
using Ligueylou.Server.Response;
using Microsoft.AspNetCore.Mvc;

namespace Ligueylou.Server.Services.Utilisateurs
{
    public interface IUtilisateurService
    {
        Task<UserRegisterResponse> Register(CreateUtilisateurDto createUtilisateurDto);
        Task<UserLoginResponse> Login(LoginRequestDto loginRequest);
        Task<UtilisateurDto> UpdateUtilisateur(Guid id, CreateUtilisateurDto dto);
        Task DeleteUtilisateur(Guid id);
        Task<UtilisateurDto> GetUtilisateurById(Guid id);
        Task<UtilisateurDto> GetUtilisateurByEmail(string email);
        Task<UtilisateurDto> GetUtilisateurByTelephone(string telephone);
        Task<IEnumerable<UtilisateurDto>> GetAllUtilisateurs();
        Task<UtilisateurDto> AddUtilisateur(CreateUtilisateurDto utilisateurDto);
    }
}
