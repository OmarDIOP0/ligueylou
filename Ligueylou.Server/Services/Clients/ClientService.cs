using Ligueylou.Server.Dtos;
using Ligueylou.Server.Models;
using Ligueylou.Server.Repository;
using Ligueylou.Server.Request;

namespace Ligueylou.Server.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly ClientRepo _repo;

        public ClientService(ClientRepo repo)
        {
            _repo = repo;
        }

        private static ClientDto MapToDto(Client c) =>
            new ClientDto
            {
                Id = c.Id,
                Nom = c.Nom,
                Prenom = c.Prenom,
                Email = c.Email,
                Telephone = c.Telephone,
                Sexe = c.Sexe,
                Role = c.Role,
                Actif = c.Actif,
                Adresse = c.Adresse,
                DateCreation = c.DateCreation,

                EvaluationsCount = c.Evaluations?.Count ?? 0,
                ReservationsCount = c.Reservations?.Count ?? 0,
                FavorisCount = c.Favoris?.Count ?? 0
            };

        public async Task<IEnumerable<ClientDto>> GetAllClients()
        {
            var clients = await _repo.GetAllClients();
            return clients.Select(MapToDto);
        }

        public async Task<ClientDto?> GetClientById(Guid id)
        {
            var c = await _repo.GetClientById(id);
            return c == null ? null : MapToDto(c);
        }

        public async Task<ClientDto> AddClient(CreateUtilisateurDto dto)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Prenom = dto.Prenom,
                Nom = dto.Nom,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Sexe = dto.Sexe,
                Role = dto.Role,
                Actif = true,
                DateCreation = DateTime.UtcNow
            };

            await _repo.AddClient(client);
            return MapToDto(client);
        }

        public async Task<ClientDto?> UpdateClient(Guid id, CreateUtilisateurDto dto)
        {
            var client = await _repo.GetClientById(id);
            if (client == null) return null;

            client.Nom = dto.Nom;
            client.Prenom = dto.Prenom;
            client.Email = dto.Email;
            client.Telephone = dto.Telephone;
            client.Sexe = dto.Sexe;

            await _repo.UpdateClient(client);
            return MapToDto(client);
        }

        public async Task<bool> DeleteClient(Guid id)
        {
            return await _repo.DeleteClient(id);
        }

        public async Task<ClientDto?> GetClientByEmail(string email)
        {
            var c = await _repo.GetClientByEmail(email);
            return c == null ? null : MapToDto(c);
        }

        public async Task<ClientDto?> GetClientByTelephone(string telephone)
        {
            var c = await _repo.GetClientByTelephone(telephone);
            return c == null ? null : MapToDto(c);
        }

        public async Task<ClientDto?> UpdateAdresse(Guid id, Adresse adresse)
        {
            var c = await _repo.UpdateClientAdresse(id, adresse);
            return c == null ? null : MapToDto(c);
        }

        public async Task<IEnumerable<Favori>> GetFavoris(Guid clientId)
        {
            return await _repo.GetFavoris(clientId);
        }

        public async Task<ClientDto?> AddFavori(Guid clientId, Favori favori)
        {
            var c = await _repo.AddClientFavori(clientId, favori);
            return c == null ? null : MapToDto(c);
        }

        public async Task<bool> RemoveFavori(Guid clientId, Guid favoriId)
        {
            return await _repo.RemoveFavori(clientId, favoriId);
        }

        public async Task<IEnumerable<Reservation>> GetReservations(Guid clientId)
        {
            return await _repo.GetClientReservations(clientId);
        }

        public async Task<ClientDto?> AddReservation(Guid clientId, Reservation reservation)
        {
            var c = await _repo.AddClientReservation(clientId, reservation);
            return c == null ? null : MapToDto(c);
        }

        public async Task<IEnumerable<Evaluation>> GetEvaluations(Guid clientId)
        {
            return await _repo.GetClientEvaluations(clientId);
        }

        public async Task<ClientDto?> AddEvaluation(Guid clientId, Evaluation evaluation)
        {
            var c = await _repo.AddClientEvaluation(clientId, evaluation);
            return c == null ? null : MapToDto(c);
        }

        public async Task<IEnumerable<ClientDto>> FilterClients(bool? actif, string? email, string? telephone)
        {
            var result = await _repo.FilterClients(actif, email, telephone);
            return result.Select(MapToDto);
        }
    }
}
