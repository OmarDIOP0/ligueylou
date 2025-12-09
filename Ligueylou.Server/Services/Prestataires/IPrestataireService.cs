using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;

namespace Ligueylou.Server.Services.Prestataires
{
    public interface IPrestataireService
    {
        Task<PrestataireDto> AddPrestataire(Prestataire prestataire);
        Task<PrestataireDto?> GetPrestataireById(Guid id);
        Task<IEnumerable<PrestataireDto>> GetAllPrestataires();
        Task<PrestataireDto?> UpdatePrestataire(Guid id, Prestataire updated);
        Task<bool> DeletePrestataire(Guid id);

        Task<PrestataireDto?> UpdateAdresse(Guid id, Adresse adresse);
        Task<PrestataireDto?> AddSpecialite(Guid id, Specialite specialite);
        Task<PrestataireDto?> AddReservation(Guid id, Reservation reservation);

        Task<bool> IsActif(Guid id);
        Task<PrestataireDto?> ActivatePrestataire(Guid id);

        Task<double?> GetScore(Guid id);
        Task<PrestataireDto?> UpdateScore(Guid id);

        Task<IEnumerable<PrestataireDto>> SearchByName(string name);
        Task<IEnumerable<PrestataireDto>> Filter(string? ville, string? specialite, bool? actif);
        Task<IEnumerable<PrestataireDto>> GetTopRated(int topN);

        Task<IEnumerable<PrestataireDto>> GetPaged(int page, int size);
    }

}
