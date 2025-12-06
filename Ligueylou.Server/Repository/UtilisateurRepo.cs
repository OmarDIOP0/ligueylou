using Ligueylou.Server.Data;
using Ligueylou.Server.Models.abstracts;
using Microsoft.EntityFrameworkCore;

namespace Ligueylou.Server.Repository
{
    public class UtilisateurRepo
    {
        private readonly ApplicationDbContext _context;
        public UtilisateurRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Utilisateur?> GetUtilisateurById(Guid id)
        {
            return await _context.Utilisateurs.FindAsync(id).AsTask();
        }
        public async Task<Utilisateur?> GetUtilisateurByEmail(string email)
        {
            return await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }
        public async Task AddUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();
        }
    }
}
