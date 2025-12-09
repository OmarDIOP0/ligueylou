using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Repository;
using Ligueylou.Server.Request;
using Microsoft.Extensions.Logging;

namespace Ligueylou.Server.Services.Prestataires
{
    public class PrestataireService : IPrestataireService
    {
        private readonly ILogger<PrestataireService> _logger;
        private readonly PrestataireRepo _prestataireRepo;

        public PrestataireService(
            ILogger<PrestataireService> logger,
            PrestataireRepo prestataireRepo
        )
        {
            _logger = logger;
            _prestataireRepo = prestataireRepo;
        }

        // --------------------------------------------------------------------
        // CREATE
        // --------------------------------------------------------------------
        public async Task<PrestataireDto> AddPrestataire(Prestataire prestataire)
        {
            if (prestataire == null)
                throw new ArgumentNullException(nameof(prestataire));

            prestataire.DateCreation = DateTime.UtcNow;
            prestataire.Actif = true;

            await _prestataireRepo.AddPrestataire(prestataire);

            return MapToDto(prestataire);
        }

        // --------------------------------------------------------------------
        // GET BY ID
        // --------------------------------------------------------------------
        public async Task<PrestataireDto?> GetPrestataireById(Guid id)
        {
            var p = await _prestataireRepo.GetPrestataireById(id);

            if (p == null)
            {
                _logger.LogWarning("Prestataire avec ID {Id} non trouvé", id);
                return null;
            }

            return MapToDto(p);
        }

        // --------------------------------------------------------------------
        // GET ALL
        // --------------------------------------------------------------------
        public async Task<IEnumerable<PrestataireDto>> GetAllPrestataires()
        {
            var list = await _prestataireRepo.GetAllPrestataires();
            return list.Select(MapToDto).ToList();
        }

        // --------------------------------------------------------------------
        // UPDATE
        // --------------------------------------------------------------------
        public async Task<PrestataireDto?> UpdatePrestataire(Guid id, Prestataire updatedData)
        {
            var prestataire = await _prestataireRepo.GetPrestataireById(id);
            if (prestataire == null)
                throw new Exception("Prestataire non trouvé");

            prestataire.Prenom = updatedData.Prenom;
            prestataire.Nom = updatedData.Nom;
            prestataire.Email = updatedData.Email;
            prestataire.Telephone = updatedData.Telephone;
            prestataire.Sexe = updatedData.Sexe;

            await _prestataireRepo.UpdatePrestataire(prestataire);

            return MapToDto(prestataire);
        }

        // --------------------------------------------------------------------
        // DELETE (LOGIQUE)
        // --------------------------------------------------------------------
        public async Task<bool> DeletePrestataire(Guid id)
        {
            return await _prestataireRepo.DeletePrestataire(id);
        }

        // --------------------------------------------------------------------
        // ADRESSE
        // --------------------------------------------------------------------
        public async Task<PrestataireDto?> UpdateAdresse(Guid id, Adresse adresse)
        {
            var p = await _prestataireRepo.UpdatePrestataireAdresse(id, adresse);
            return p != null ? MapToDto(p) : null;
        }

        // --------------------------------------------------------------------
        // SPECIALITES
        // --------------------------------------------------------------------
        public async Task<PrestataireDto?> AddSpecialite(Guid id, Specialite specialite)
        {
            var p = await _prestataireRepo.AddPrestataireSpecialite(id, specialite);
            return p != null ? MapToDto(p) : null;
        }

        // --------------------------------------------------------------------
        // RESERVATIONS
        // --------------------------------------------------------------------
        public async Task<PrestataireDto?> AddReservation(Guid id, Reservation reservation)
        {
            var p = await _prestataireRepo.AddPrestataireReservation(id, reservation);
            return p != null ? MapToDto(p) : null;
        }

        // --------------------------------------------------------------------
        // ÉTAT / ACTIVATION
        // --------------------------------------------------------------------
        public async Task<bool> IsActif(Guid id)
        {
            return await _prestataireRepo.IsPrestataireActif(id);
        }

        public async Task<PrestataireDto?> ActivatePrestataire(Guid id)
        {
            var p = await _prestataireRepo.ActivatePrestataire(id);
            return p != null ? MapToDto(p) : null;
        }

        // --------------------------------------------------------------------
        // SCORE
        // --------------------------------------------------------------------
        public async Task<double?> GetScore(Guid id)
        {
            return await _prestataireRepo.GetPrestataireScore(id);
        }

        public async Task<PrestataireDto?> UpdateScore(Guid id)
        {
            var p = await _prestataireRepo.UpdatePrestataireScore(id);
            return p != null ? MapToDto(p) : null;
        }

        // --------------------------------------------------------------------
        // SEARCH / FILTER
        // --------------------------------------------------------------------
        public async Task<IEnumerable<PrestataireDto>> SearchByName(string name)
        {
            var list = await _prestataireRepo.SearchPrestatairesByName(name);
            return list.Select(MapToDto).ToList();
        }

        public async Task<IEnumerable<PrestataireDto>> Filter(
            string? ville = null,
            string? specialite = null,
            bool? actif = null)
        {
            var list = await _prestataireRepo.FilterPrestataires(ville, specialite, actif);
            return list.Select(MapToDto).ToList();
        }

        public async Task<IEnumerable<PrestataireDto>> GetTopRated(int topN)
        {
            var list = await _prestataireRepo.GetTopRatedPrestataires(topN);
            return list.Select(MapToDto).ToList();
        }

        // --------------------------------------------------------------------
        // PAGINATION
        // --------------------------------------------------------------------
        public async Task<IEnumerable<PrestataireDto>> GetPaged(int page, int size)
        {
            var list = await _prestataireRepo.GetPrestatairesPaged(page, size);
            return list.Select(MapToDto).ToList();
        }

        // --------------------------------------------------------------------
        // MAPPING DTO
        // --------------------------------------------------------------------
        private PrestataireDto MapToDto(Prestataire p)
        {
            return new PrestataireDto
            {
                Id = p.Id,
                Prenom = p.Prenom,
                Nom = p.Nom,
                Email = p.Email,
                Telephone = p.Telephone,
                Sexe = p.Sexe,
                Role = p.Role,
                Actif = p.Actif,
                DateCreation = p.DateCreation,
                DerniereConnexion = p.DerniereConnexion,

                Score = p.Score,
                ServicesCount = p.Services?.Count ?? 0,
                ReservationsCount = p.Reservations?.Count ?? 0,
                SpecialitesCount = p.Specialites?.Count ?? 0,
                FavorisCount = p.Favoris?.Count ?? 0
            };
        }
    }
}
