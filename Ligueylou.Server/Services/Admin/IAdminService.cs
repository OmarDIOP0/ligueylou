using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Request;

namespace Ligueylou.Server.Services.Administrateurs
{
    public interface IAdminService
    {
        Task<IEnumerable<AdministrateurDto>> GetAllAdmins();
        Task<AdministrateurDto?> GetAdminById(Guid id);
        Task<AdministrateurDto> AddAdmin(CreateUtilisateurDto dto);
        Task<AdministrateurDto?> UpdateAdmin(Guid id, CreateUtilisateurDto dto);
        Task<bool> DeleteAdmin(Guid id);

        Task<AdministrateurDto?> GetAdminByEmail(string email);
        Task<AdministrateurDto?> GetAdminByTelephone(string telephone);

        Task<AdministrateurDto?> UpdateAdresse(Guid id, Adresse adresse);

        Task<bool> VerifyCodeSecret(Guid adminId, string codeSecret);
        Task<bool> UpdateCodeSecret(Guid adminId, string newCode);

        Task<IEnumerable<AdministrateurDto>> FilterAdmins(string? ville, bool? actif);
    }
}
