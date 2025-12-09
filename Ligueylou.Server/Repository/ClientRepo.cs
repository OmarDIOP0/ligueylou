using Ligueylou.Server.Data;
using Ligueylou.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Ligueylou.Server.Repository
{
    public class ClientRepo
    {
        private readonly ApplicationDbContext _context;

        public ClientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // -----------------------------
        //  Helper pour inclure toutes les relations
        // -----------------------------
        private IQueryable<Client> QueryClients()
        {
            return _context.Clients
                .Include(c => c.Evaluations)
                .Include(c => c.Reservations)
                .Include(c => c.Favoris);
        }

        // -----------------------------
        // BASIC CRUD
        // -----------------------------
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await QueryClients().ToListAsync();
        }

        public async Task<Client?> GetClientById(Guid id)
        {
            return await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return false;

            client.Actif = false; // désactivation logique
            await _context.SaveChangesAsync();
            return true;
        }

        // -----------------------------
        // RECHERCHES SPÉCIFIQUES
        // -----------------------------
        public async Task<Client?> GetClientByEmail(string email)
        {
            return await QueryClients()
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client?> GetClientByTelephone(string telephone)
        {
            return await QueryClients()
                .FirstOrDefaultAsync(c => c.Telephone == telephone);
        }

        public async Task<IEnumerable<Client>> SearchClientsByName(string name)
        {
            return await QueryClients()
                .Where(c => c.Nom.Contains(name) || c.Prenom.Contains(name))
                .ToListAsync();
        }
        // ---------------------------
        // ADRESSE
        // ---------------------------
        public async Task<Client?> UpdateClientAdresse(Guid clientId, Adresse adresse)
        {
            var client = await _context.Clients
                .Include(c => c.Adresse)
                .FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null)
                return null;

            client.Adresse = adresse;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsByAdresse(string ville, string pays)
        {
            return await QueryClients()
                .Where(c => c.Adresse != null &&
                            c.Adresse.Ville == ville &&
                            c.Adresse.Pays == pays)
                .ToListAsync();
        }

        // -----------------------------
        // FAVORIS
        // -----------------------------
        public async Task<Client?> AddClientFavori(Guid clientId, Favori favori)
        {
            var client = await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null) return null;

            client.Favoris.Add(favori);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<IEnumerable<Favori>> GetFavoris(Guid clientId)
        {
            var client = await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            return client?.Favoris ?? new List<Favori>();
        }
        public async Task<bool> RemoveFavori(Guid clientId, Guid favoriId)
        {
            var client = await QueryClients().FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null)
                return false;

            var favori = client.Favoris.FirstOrDefault(f => f.Id == favoriId);

            if (favori == null)
                return false;

            client.Favoris.Remove(favori);
            await _context.SaveChangesAsync();

            return true;
        }
        // -----------------------------
        // RESERVATIONS
        // -----------------------------
        public async Task<Client?> AddClientReservation(Guid clientId, Reservation reservation)
        {
            var client = await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null) return null;

            client.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<IEnumerable<Reservation>> GetClientReservations(Guid clientId)
        {
            var client = await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            return client?.Reservations ?? new List<Reservation>();
        }
        public async Task<IEnumerable<Client>> GetClientsByReservation(Guid reservationId)
        {
            return await QueryClients()
                .Where(c => c.Reservations.Any(r => r.Id == reservationId))
                .ToListAsync();
        }

        // -----------------------------
        // EVALUATIONS
        // -----------------------------
        public async Task<Client?> AddClientEvaluation(Guid clientId, Evaluation evaluation)
        {
            var client = await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null) return null;

            client.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();
            var service = await _context.Services
                            .Include(s => s.Prestataire)
                            .Include(s => s.Evaluations)
                            .FirstOrDefaultAsync(s => s.Id == evaluation.ServiceId);

            if (service != null)
            {
                var prestataire = service.Prestataire;
                var allEvaluations = _context.Evaluations
                    .Where(e => e.Service.PrestataireId == prestataire.Id);

                prestataire.Score = allEvaluations.Any() ? await allEvaluations.AverageAsync(e => e.Score) : 0;
                await _context.SaveChangesAsync();
            }

            return client;
        }

        public async Task<IEnumerable<Evaluation>> GetClientEvaluations(Guid clientId)
        {
            var client = await QueryClients()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            return client?.Evaluations ?? new List<Evaluation>();
        }

        // -----------------------------
        // ACTIVATION
        // -----------------------------
        public async Task<bool> IsClientActif(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client != null && client.Actif;
        }

        public async Task<Client?> ActivateClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return null;

            client.Actif = true;
            await _context.SaveChangesAsync();
            return client;
        }

        // -----------------------------
        // STATISTIQUES
        // -----------------------------
        public async Task<int> GetClientCount()
        {
            return await _context.Clients.CountAsync();
        }

        public async Task<int> CountClientsActifs()
        {
            return await _context.Clients.CountAsync(c => c.Actif);
        }

        public async Task<int> CountReservationsByClient(Guid clientId)
        {
            return await _context.Reservations
                .CountAsync(r => r.ClientId == clientId);
        }

        // -----------------------------
        // PAGINATION
        // -----------------------------
        public async Task<IEnumerable<Client>> GetClientsPaged(int page, int size)
        {
            return await QueryClients()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // -----------------------------
        // FILTRAGE AVANCÉ
        // -----------------------------
        public async Task<IEnumerable<Client>> FilterClients(
            bool? actif = null,
            string? email = null,
            string? telephone = null)
        {
            var query = QueryClients();

            if (actif != null)
                query = query.Where(c => c.Actif == actif);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email == email);

            if (!string.IsNullOrEmpty(telephone))
                query = query.Where(c => c.Telephone == telephone);

            return await query.ToListAsync();
        }
    }
}
