using Ligueylou.Server.Data;
using Ligueylou.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Ligueylou.Server.Repository
{
    public class PrestataireRepo
    {
        private readonly ApplicationDbContext _context;

        public PrestataireRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Prestataire> QueryPrestataires()
        {
            return _context.Prestataires
                .Include(p => p.Services)
                .Include(p => p.Specialites)
                .Include(p => p.Reservations)
                .Include(p => p.Favoris)
                .Include(p => p.Adresse);
        }

        // -----------------------------
        // BASIC CRUD
        // -----------------------------
        public async Task<IEnumerable<Prestataire>> GetAllPrestataires()
        {
            return await QueryPrestataires().ToListAsync();
        }

        public async Task<Prestataire?> GetPrestataireById(Guid id)
        {
            return await QueryPrestataires()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPrestataire(Prestataire prestataire)
        {
            await _context.Prestataires.AddAsync(prestataire);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrestataire(Prestataire prestataire)
        {
            _context.Prestataires.Update(prestataire);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePrestataire(Guid id)
        {
            var prestataire = await _context.Prestataires.FindAsync(id);
            if (prestataire == null)
                return false;

            prestataire.Actif = false; // désactivation logique
            await _context.SaveChangesAsync();
            return true;
        }

        // -----------------------------
        // RECHERCHES SPÉCIFIQUES
        // -----------------------------
        public async Task<Prestataire?> GetPrestataireByEmail(string email)
        {
            return await QueryPrestataires()
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Prestataire?> GetPrestataireByTelephone(string telephone)
        {
            return await QueryPrestataires()
                .FirstOrDefaultAsync(p => p.Telephone == telephone);
        }

        public async Task<IEnumerable<Prestataire>> SearchPrestatairesByName(string name)
        {
            return await QueryPrestataires()
                .Where(p => p.Prenom.Contains(name) || p.Nom.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Prestataire>> GetPrestatairesBySpecialite(string specialite)
        {
            return await QueryPrestataires()
                .Where(p => p.Specialites.Any(s => s.Libelle == specialite))
                .ToListAsync();
        }

        public async Task<IEnumerable<Prestataire>> GetPrestatairesByAdresse(string ville, string pays)
        {
            return await QueryPrestataires()
                .Where(p => p.Adresse != null &&
                            p.Adresse.Ville == ville &&
                            p.Adresse.Pays == pays)
                .ToListAsync();
        }

        // -----------------------------
        // ADRESSE
        // -----------------------------
        public async Task<Prestataire?> UpdatePrestataireAdresse(Guid id, Adresse adresse)
        {
            var prestataire = await _context.Prestataires
                .Include(p => p.Adresse)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestataire == null)
                return null;

            prestataire.Adresse = adresse;
            await _context.SaveChangesAsync();
            return prestataire;
        }

        // -----------------------------
        // SPECIALITES
        // -----------------------------
        public async Task<Prestataire?> AddPrestataireSpecialite(Guid id, Specialite specialite)
        {
            var prestataire = await _context.Prestataires
                .Include(p => p.Specialites)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestataire == null)
                return null;

            prestataire.Specialites.Add(specialite);
            await _context.SaveChangesAsync();

            return prestataire;
        }

        // -----------------------------
        // RESERVATIONS
        // -----------------------------
        public async Task<Prestataire?> AddPrestataireReservation(Guid id, Reservation reservation)
        {
            var prestataire = await _context.Prestataires
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestataire == null)
                return null;

            prestataire.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return prestataire;
        }

        public async Task<IEnumerable<Prestataire>> GetPrestatairesByReservation(Guid reservationId)
        {
            return await QueryPrestataires()
                .Where(p => p.Reservations.Any(r => r.Id == reservationId))
                .ToListAsync();
        }

        // -----------------------------
        // ACTIVATION
        // -----------------------------
        public async Task<bool> IsPrestataireActif(Guid id)
        {
            var p = await _context.Prestataires.FindAsync(id);
            return p != null && p.Actif;
        }

        public async Task<Prestataire?> ActivatePrestataire(Guid id)
        {
            var p = await _context.Prestataires.FindAsync(id);
            if (p == null) return null;

            p.Actif = true;
            await _context.SaveChangesAsync();
            return p;
        }

        // -----------------------------
        // SCORE & EVALUATIONS
        // -----------------------------
        public async Task<double?> GetPrestataireScore(Guid id)
        {
            return await _context.Prestataires
                .Where(p => p.Id == id)
                .Select(p => p.Score)
                .FirstOrDefaultAsync();
        }

        public async Task<Prestataire?> UpdatePrestataireScore(Guid prestataireId)
        {
            var prestataire = await _context.Prestataires
                        .Include(p => p.Services)
                        .ThenInclude(s => s.Evaluations)
                        .FirstOrDefaultAsync(p => p.Id == prestataireId);
            if (prestataire == null) return null;

            var evaluations = prestataire.Services
                        .SelectMany(s => s.Evaluations)
                        .ToList();
            if(evaluations.Count == 0)
            {
                prestataire.Score = 0;
            }
            else
            {
                prestataire.Score = evaluations.Average(e => e.Score);

            }
            await _context.SaveChangesAsync();

            return prestataire;
        }

        public async Task<IEnumerable<Prestataire>> GetTopRatedPrestataires(int topN)
        {
            return await QueryPrestataires()
                .OrderByDescending(p => p.Score)
                .Take(topN)
                .ToListAsync();
        }

        // -----------------------------
        // STATISTIQUES
        // -----------------------------
        public async Task<int> GetPrestataireCount()
        {
            return await _context.Prestataires.CountAsync();
        }

        public async Task<int> CountPrestatairesActifs()
        {
            return await _context.Prestataires.CountAsync(p => p.Actif);
        }

        // -----------------------------
        // PAGINATION
        // -----------------------------
        public async Task<IEnumerable<Prestataire>> GetPrestatairesPaged(int page, int size)
        {
            return await QueryPrestataires()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // -----------------------------
        // FILTRAGE COMBINÉ (TRÈS UTILE)
        // -----------------------------
        public async Task<IEnumerable<Prestataire>> FilterPrestataires(
            string? ville = null,
            string? specialite = null,
            bool? actif = null)
        {
            var query = QueryPrestataires();

            if (!string.IsNullOrEmpty(ville))
                query = query.Where(p => p.Adresse.Ville == ville);

            if (!string.IsNullOrEmpty(specialite))
                query = query.Where(p => p.Specialites.Any(s => s.Libelle == specialite));

            if (actif != null)
                query = query.Where(p => p.Actif == actif);

            return await query.ToListAsync();
        }
    }
}
