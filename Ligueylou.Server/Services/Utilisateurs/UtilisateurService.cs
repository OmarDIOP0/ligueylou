using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Models.abstracts;
using Ligueylou.Server.Repository;
using Ligueylou.Server.Request;
using Microsoft.Extensions.Logging;

namespace Ligueylou.Server.Services.Utilisateurs
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly ILogger<UtilisateurService> _logger;
        private readonly UtilisateurRepo _utilisateurRepo;

        public UtilisateurService(
            ILogger<UtilisateurService> logger,
            UtilisateurRepo utilisateurRepo
        )
        {
            _logger = logger;
            _utilisateurRepo = utilisateurRepo;
        }
        public async Task<UtilisateurDto?> GetUtilisateurById(Guid id)
        {
            var user = await _utilisateurRepo.GetUtilisateurById(id);

            if (user == null)
            {
                _logger.LogWarning("Utilisateur avec ID {Id} non trouvé", id);
                return null;
            }

            return MapToDto(user);
        }

        public async Task<UtilisateurDto?> GetUtilisateurByEmail(string email)
        {
            var user = await _utilisateurRepo.GetUtilisateurByEmail(email);

            if (user == null)
            {
                _logger.LogWarning("Utilisateur email {email} non trouvé", email);
                return null;
            }

            return MapToDto(user);
        }

        public async Task<IEnumerable<UtilisateurDto>> GetAllUtilisateurs()
        {
            var users = await _utilisateurRepo.GetAllUtilisateurs();

            return users.Select(MapToDto).ToList();
        }

        public async Task<UtilisateurDto> AddUtilisateur(CreateUtilisateurDto dto)
        {
            Utilisateur user = dto.Role switch
            {
                Models.Enums.RoleEnum.CLIENT => new Client(),
                Models.Enums.RoleEnum.PRESTATAIRE => new Prestataire(),
                Models.Enums.RoleEnum.ADMIN => new Administrateur
                {
                    CodeSecret = Guid.NewGuid().ToString("N")[..8]
                },
                _ => throw new ArgumentException("Role invalide")
            };

            user.Prenom = dto.Prenom;
            user.Nom = dto.Nom;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Telephone = dto.Telephone;
            user.Sexe = dto.Sexe;
            user.Role = dto.Role;
            user.Actif = true;
            user.DateCreation = DateTime.UtcNow;

            await _utilisateurRepo.AddUtilisateur(user);

            return MapToDto(user);
        }
        private UtilisateurDto MapToDto(Utilisateur user)
        {
            return user switch
            {
                Client c => new ClientDto
                {
                    Id = c.Id,
                    Prenom = c.Prenom,
                    Nom = c.Nom,
                    Email = c.Email,
                    Telephone = c.Telephone,
                    Role = c.Role,
                    Actif = c.Actif,
                    DateCreation = c.DateCreation,
                    DerniereConnexion = c.DerniereConnexion,
                    EvaluationsCount = c.Evaluations.Count,
                    ReservationsCount = c.Reservations.Count,
                    FavorisCount = c.Favoris.Count
                },

                Prestataire p => new PrestataireDto
                {
                    Id = p.Id,
                    Prenom = p.Prenom,
                    Nom = p.Nom,
                    Email = p.Email,
                    Telephone = p.Telephone,
                    Role = p.Role,
                    Actif = p.Actif,
                    DateCreation = p.DateCreation,
                    DerniereConnexion = p.DerniereConnexion,
                    Score = p.Score,
                    ServicesCount = p.Services.Count,
                    ReservationsCount = p.Reservations.Count,
                    SpecialitesCount = p.Specialites.Count,
                    FavorisCount = p.Favoris.Count
                },

                Administrateur a => new AdministrateurDto
                {
                    Id = a.Id,
                    Prenom = a.Prenom,
                    Nom = a.Nom,
                    Email = a.Email,
                    Telephone = a.Telephone,
                    Role = a.Role,
                    Actif = a.Actif,
                    DateCreation = a.DateCreation,
                    DerniereConnexion = a.DerniereConnexion,
                    CodeSecret = a.CodeSecret
                },

                _ => throw new ArgumentException("Type utilisateur inconnu")
            };
        }
    }
}
