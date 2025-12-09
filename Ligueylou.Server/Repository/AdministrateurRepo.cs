using Ligueylou.Server.Data;
using Ligueylou.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Ligueylou.Server.Repository
{
    public class AdministrateurRepo
    {
        private readonly ApplicationDbContext _context;

        public AdministrateurRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Base query avec Include
        private IQueryable<Administrateur> QueryAdmins()
        {
            return _context.Administrateurs
                .Include(a => a.Adresse);
        }

        // -----------------------------------------
        // CRUD
        // -----------------------------------------
        public async Task<IEnumerable<Administrateur>> GetAllAdmins()
        {
            return await QueryAdmins().ToListAsync();
        }

        public async Task<Administrateur?> GetAdminById(Guid id)
        {
            return await QueryAdmins().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAdmin(Administrateur admin)
        {
            admin.DateCreation = DateTime.Now;
            await _context.Administrateurs.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdmin(Administrateur admin)
        {
            _context.Administrateurs.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAdmin(Guid id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin == null)
                return false;

            admin.Actif = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // -----------------------------------------
        // Recherche
        // -----------------------------------------
        public async Task<Administrateur?> GetAdminByEmail(string email)
        {
            return await QueryAdmins()
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Administrateur?> GetAdminByTelephone(string tel)
        {
            return await QueryAdmins()
                .FirstOrDefaultAsync(a => a.Telephone == tel);
        }

        public async Task<IEnumerable<Administrateur>> SearchAdminsByName(string name)
        {
            return await QueryAdmins()
                .Where(a => a.Nom.Contains(name) || a.Prenom.Contains(name))
                .ToListAsync();
        }

        // -----------------------------------------
        // Code Secret
        // -----------------------------------------
        public async Task<bool> VerifyCodeSecret(Guid adminId, string codeSecret)
        {
            var admin = await _context.Administrateurs
                .FirstOrDefaultAsync(a => a.Id == adminId);

            if (admin == null)
                return false;

            return admin.CodeSecret == codeSecret;
        }

        public async Task<bool> UpdateCodeSecret(Guid adminId, string newCode)
        {
            var admin = await _context.Administrateurs.FindAsync(adminId);
            if (admin == null)
                return false;

            admin.CodeSecret = newCode;
            await _context.SaveChangesAsync();
            return true;
        }

        // -----------------------------------------
        // Adresse
        // -----------------------------------------
        public async Task<Administrateur?> UpdateAdminAdresse(Guid id, Adresse adresse)
        {
            var admin = await QueryAdmins().FirstOrDefaultAsync(a => a.Id == id);

            if (admin == null)
                return null;

            admin.Adresse = adresse;
            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<IEnumerable<Administrateur>> GetAdminsByAdresse(string ville, string pays)
        {
            return await QueryAdmins()
                .Where(a => a.Adresse != null &&
                            a.Adresse.Ville == ville &&
                            a.Adresse.Pays == pays)
                .ToListAsync();
        }

        // -----------------------------------------
        // Activation
        // -----------------------------------------
        public async Task<bool> IsAdminActif(Guid id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            return admin != null && admin.Actif;
        }

        public async Task<Administrateur?> ActivateAdmin(Guid id)
        {
            var admin = await _context.Administrateurs.FindAsync(id);
            if (admin == null)
                return null;

            admin.Actif = true;
            await _context.SaveChangesAsync();
            return admin;
        }

        // -----------------------------------------
        // Pagination
        // -----------------------------------------
        public async Task<IEnumerable<Administrateur>> GetAdminsPaged(int page, int size)
        {
            return await QueryAdmins()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        // -----------------------------------------
        // Filtrage combiné
        // -----------------------------------------
        public async Task<IEnumerable<Administrateur>> FilterAdmins(
            string? ville = null,
            bool? actif = null)
        {
            var query = QueryAdmins();

            if (!string.IsNullOrEmpty(ville))
                query = query.Where(a => a.Adresse != null && a.Adresse.Ville == ville);

            if (actif != null)
                query = query.Where(a => a.Actif == actif);

            return await query.ToListAsync();
        }

        // -----------------------------------------
        // Statistiques
        // -----------------------------------------
        public async Task<int> GetAdminCount()
        {
            return await _context.Administrateurs.CountAsync();
        }

        public async Task<int> GetActiveAdminCount()
        {
            return await _context.Administrateurs.CountAsync(a => a.Actif);
        }
    }
}
