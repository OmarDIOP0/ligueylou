using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Request;

namespace Ligueylou.Server.Services.Clients
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClients();
        Task<ClientDto?> GetClientById(Guid id);
        Task<ClientDto> AddClient(CreateUtilisateurDto dto);
        Task<ClientDto?> UpdateClient(Guid id, CreateUtilisateurDto dto);
        Task<bool> DeleteClient(Guid id);

        Task<ClientDto?> GetClientByEmail(string email);
        Task<ClientDto?> GetClientByTelephone(string telephone);

        Task<ClientDto?> UpdateAdresse(Guid id, Adresse adresse);

        Task<IEnumerable<Favori>> GetFavoris(Guid clientId);
        Task<ClientDto?> AddFavori(Guid clientId, Favori favori);
        Task<bool> RemoveFavori(Guid clientId, Guid favoriId);

        Task<IEnumerable<Reservation>> GetReservations(Guid clientId);
        Task<ClientDto?> AddReservation(Guid clientId, Reservation reservation);

        Task<IEnumerable<Evaluation>> GetEvaluations(Guid clientId);
        Task<ClientDto?> AddEvaluation(Guid clientId, Evaluation evaluation);

        Task<IEnumerable<ClientDto>> FilterClients(bool? actif, string? email, string? telephone);
    }
}
