using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Repository;
using Ligueylou.Server.Request;

namespace Ligueylou.Server.Services.Administrateurs
{
    public class AdminService : IAdminService
    {
        private readonly AdministrateurRepo _repo;

        public AdminService(AdministrateurRepo repo)
        {
            _repo = repo;
        }

        private static AdministrateurDto MapToDto(Administrateur a) =>
            new AdministrateurDto
            {
                Id = a.Id,
                Nom = a.Nom,
                Prenom = a.Prenom,
                Email = a.Email,
                Telephone = a.Telephone,
                Sexe = a.Sexe,
                Role = a.Role,
                Actif = a.Actif,
                Adresse = a.Adresse,
                DateCreation = a.DateCreation
            };

        public async Task<IEnumerable<AdministrateurDto>> GetAllAdmins()
        {
            var admins = await _repo.GetAllAdmins();
            return admins.Select(MapToDto);
        }

        public async Task<AdministrateurDto?> GetAdminById(Guid id)
        {
            var a = await _repo.GetAdminById(id);
            return a == null ? null : MapToDto(a);
        }

        public async Task<AdministrateurDto> AddAdmin(CreateUtilisateurDto dto)
        {
            var admin = new Administrateur
            {
                Id = Guid.NewGuid(),
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Sexe = dto.Sexe,
                Role = dto.Role,
                Actif = true,
                DateCreation = DateTime.UtcNow
            };

            await _repo.AddAdmin(admin);
            return MapToDto(admin);
        }

        public async Task<AdministrateurDto?> UpdateAdmin(Guid id, CreateUtilisateurDto dto)
        {
            var admin = await _repo.GetAdminById(id);
            if (admin == null) return null;

            admin.Nom = dto.Nom;
            admin.Prenom = dto.Prenom;
            admin.Email = dto.Email;
            admin.Telephone = dto.Telephone;
            admin.Sexe = dto.Sexe;

            await _repo.UpdateAdmin(admin);
            return MapToDto(admin);
        }

        public async Task<bool> DeleteAdmin(Guid id)
        {
            return await _repo.DeleteAdmin(id);
        }

        public async Task<AdministrateurDto?> GetAdminByEmail(string email)
        {
            var a = await _repo.GetAdminByEmail(email);
            return a == null ? null : MapToDto(a);
        }

        public async Task<AdministrateurDto?> GetAdminByTelephone(string telephone)
        {
            var a = await _repo.GetAdminByTelephone(telephone);
            return a == null ? null : MapToDto(a);
        }

        public async Task<AdministrateurDto?> UpdateAdresse(Guid id, Adresse adresse)
        {
            var a = await _repo.UpdateAdminAdresse(id, adresse);
            return a == null ? null : MapToDto(a);
        }

        public async Task<bool> VerifyCodeSecret(Guid adminId, string codeSecret)
        {
            return await _repo.VerifyCodeSecret(adminId, codeSecret);
        }

        public async Task<bool> UpdateCodeSecret(Guid adminId, string newCode)
        {
            return await _repo.UpdateCodeSecret(adminId, newCode);
        }

        public async Task<IEnumerable<AdministrateurDto>> FilterAdmins(string? ville, bool? actif)
        {
            var result = await _repo.FilterAdmins(ville, actif);
            return result.Select(MapToDto);
        }
    }
}
