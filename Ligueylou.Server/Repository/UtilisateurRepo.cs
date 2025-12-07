using Ligueylou.Server.Data;
using Ligueylou.Server.Models;
using Ligueylou.Server.Models.abstracts;
using Ligueylou.Server.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Ligueylou.Server.Repository
{
    public class UtilisateurRepo
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // -----------------------------
        // GET BY ID
        // -----------------------------
        public async Task<Utilisateur?> GetUtilisateurById(Guid id)
        {
            var user = await _context.Utilisateurs
                .Include(u => u.Adresse)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return null;

            await LoadRoleNavigationProperties(user);
            return user;
        }

        // -----------------------------
        // GET BY EMAIL
        // -----------------------------
        public async Task<bool> EmailExist(string email)
        {
            return await _context.Utilisateurs.AnyAsync(u => u.Email == email);
        }
        public async Task<Utilisateur?> GetUtilisateurByEmail(string email)
        {
            var user = await _context.Utilisateurs
                .Include(u => u.Adresse)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            await LoadRoleNavigationProperties(user);
            return user;
        }

        // -----------------------------
        // GET ALL
        // -----------------------------
        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateurs()
        {
            var utilisateurs = await _context.Utilisateurs
                .Include(u => u.Adresse)
                .ToListAsync();

            foreach (var user in utilisateurs)
                await LoadRoleNavigationProperties(user);

            return utilisateurs;
        }

        // -----------------------------
        // CREATE
        // -----------------------------
        public async Task AddUtilisateur(Utilisateur utilisateur)
        {
            await _context.Utilisateurs.AddAsync(utilisateur);
            await _context.SaveChangesAsync();
        }

        // -----------------------------
        // LOADER DES PROPRIÉTÉS SELON LE RÔLE
        // -----------------------------
        private async Task LoadRoleNavigationProperties(Utilisateur user)
        {
            switch (user.Role)
            {
                case RoleEnum.CLIENT:
                    var client = user as Client;
                    if (client != null)
                    {
                        await _context.Entry(client).Collection(c => c.Evaluations).LoadAsync();
                        await _context.Entry(client).Collection(c => c.Reservations).LoadAsync();
                        await _context.Entry(client).Collection(c => c.Favoris).LoadAsync();
                    }
                    break;

                case RoleEnum.PRESTATAIRE:
                    var prestataire = user as Prestataire;
                    if (prestataire != null)
                    {
                        await _context.Entry(prestataire).Collection(p => p.Services).LoadAsync();
                        await _context.Entry(prestataire).Collection(p => p.Specialites).LoadAsync();
                        await _context.Entry(prestataire).Collection(p => p.Reservations).LoadAsync();
                        await _context.Entry(prestataire).Collection(p => p.Favoris).LoadAsync();
                    }
                    break;

                case RoleEnum.ADMIN:
                    // L'admin n’a pas de relations lourdes → rien à charger
                    break;
            }
        }
    }
}
