using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Models.abstracts;
using Ligueylou.Server.Repository;
using Ligueylou.Server.Request;
using Ligueylou.Server.Response;
using Ligueylou.Server.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Ligueylou.Server.Services.Utilisateurs
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly ILogger<UtilisateurService> _logger;
        private readonly UtilisateurRepo _utilisateurRepo;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public UtilisateurService(
            ILogger<UtilisateurService> logger,
            UtilisateurRepo utilisateurRepo,
            ITokenService tokenService,
            IConfiguration config
        )
        {
            _logger = logger;
            _utilisateurRepo = utilisateurRepo;
            _tokenService = tokenService;
            _config = config;
        }
        public async Task<UserRegisterResponse> Register(CreateUtilisateurDto request)
        {
            if(request == null)
                throw new Exception(nameof(request));
            if (!string.IsNullOrEmpty(request.Email) && await _utilisateurRepo.EmailExist(request.Email))
                throw new Exception("L'adresse email est deja utilisé");

            if (!string.IsNullOrEmpty(request.Telephone) && await _utilisateurRepo.TelephoneExist(request.Telephone))
                throw new Exception("Le numéro de téléphone deja utilisé");

            Utilisateur user = request.Role switch
            {
                Models.Enums.RoleEnum.CLIENT => new Client(),
                Models.Enums.RoleEnum.PRESTATAIRE => new Prestataire(),
                Models.Enums.RoleEnum.ADMIN => new Administrateur
                {
                    CodeSecret = Guid.NewGuid().ToString("N")[..8]
                },
                _ => throw new ArgumentException("Role invalide")
            };
            user.Prenom = request.Prenom;
            user.Nom = request.Nom;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Telephone = request.Telephone;
            user.Sexe = request.Sexe;
            user.Role = request.Role;
            user.Actif = true;
            user.DateCreation = DateTime.UtcNow;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _utilisateurRepo.AddUtilisateur(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Prenom ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString().ToLower()),
                new Claim(JwtRegisteredClaimNames.GivenName,$"{user.Prenom} {user.Nom}"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var (accessToken, expire) = _tokenService.GenerateAccessToken(claims);
            string refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_config["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            var refreshTokenEntity = new RefreshToken
            {
                UserName =user.Id + user.Nom + user.Prenom,
                Refresh_Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
            };
            await _utilisateurRepo.AddRefreshToken(refreshTokenEntity);
            user = await _utilisateurRepo.GetUtilisateurById(user.Id);
            var userDto = MapToDto(user);
            return new UserRegisterResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                TokenExpireAt = expire,
                Utilisateur = userDto
            };
        }

        public async Task<UserLoginResponse> Login(LoginRequestDto loginRequest)
        {
            if (loginRequest == null)
                throw new ArgumentNullException(nameof(loginRequest));

            if (string.IsNullOrEmpty(loginRequest.Email) && string.IsNullOrEmpty(loginRequest.Telephone))
                throw new Exception("Email ou Téléphone requis");

            Utilisateur? user = null;
            if(user.Actif == false)
                throw new UnauthorizedAccessException("Compte utilisateur désactivé");
            if (!string.IsNullOrEmpty(loginRequest.Email))
                user = await _utilisateurRepo.GetUtilisateurByEmail(loginRequest.Email);
            else if (!string.IsNullOrEmpty(loginRequest.Telephone))
                user = await _utilisateurRepo.GetUtilisateurByTelephone(loginRequest.Telephone);

            if (user == null)
                throw new UnauthorizedAccessException("Email/Telephone ou Mot de passe incorrecte");

            bool passwordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
            if (!passwordValid)
                throw new UnauthorizedAccessException("Mot de passe incorrect");
            user.DerniereConnexion = DateTime.UtcNow;
            await _utilisateurRepo.UpdateUtilisateur(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Prenom ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString().ToLower()),
                new Claim(JwtRegisteredClaimNames.GivenName, $"{user.Prenom} {user.Nom}"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var (accessToken, expire) = _tokenService.GenerateAccessToken(claims);
            string refreshToken = _tokenService.GenerateRefreshToken();
            _ = int.TryParse(_config["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            var refreshTokenEntity = new RefreshToken
            {
                UserName = $"{user.Id}{user.Nom}{user.Prenom}",
                Refresh_Token = refreshToken,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
            };
            await _utilisateurRepo.AddRefreshToken(refreshTokenEntity);
            user = await _utilisateurRepo.GetUtilisateurById(user.Id);
            var userDto = MapToDto(user);

            return new UserLoginResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                TokenExpireAt = expire,
                Utilisateur = userDto
            };
        }
        public async Task<UtilisateurDto> UpdateUtilisateur(Guid id, CreateUtilisateurDto dto)
        {
            var user = await _utilisateurRepo.GetUtilisateurById(id);
            if (user == null)
                throw new Exception("Utilisateur non trouvé");

            user.Prenom = dto.Prenom;
            user.Nom = dto.Nom;
            user.Email = dto.Email;
            user.Telephone = dto.Telephone;
            user.Sexe = dto.Sexe;
            if (!string.IsNullOrEmpty(dto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _utilisateurRepo.UpdateUtilisateur(user);

            return MapToDto(user);
        }

        public async Task DeleteUtilisateur(Guid id)
        {
            await _utilisateurRepo.DeleteUtilisateur(id);
        }

        public async Task<UtilisateurDto> GetUtilisateurById(Guid id)
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
        public async Task<UtilisateurDto?> GetUtilisateurByTelephone(string telephone)
        {
            var user = await _utilisateurRepo.GetUtilisateurByTelephone(telephone);

            if (user == null)
            {
                _logger.LogWarning("Utilisateur telephone {telephone} non trouvé", telephone);
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
