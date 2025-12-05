namespace Ligueylou.Server.Data;

using Ligueylou.Server.Models;
using Ligueylou.Server.Models.abstracts;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
    {
    }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Administrateur> Administrateurs { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Prestataire> Prestataires { get; set; }
    public DbSet<Adresse> Adresses { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Specialite> Specialites { get; set; }
    public DbSet<Paiement> Paiements { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Favori> Favoris { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurer Favori avec clé composite
        modelBuilder.Entity<Favori>()
            .HasKey(f => new { f.ClientId, f.PrestataireId });

        // Relations Client - Adresse
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Adresse)
            .WithMany(a => a.Clients)
            .HasForeignKey(c => c.AdresseId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relations Prestataire - Adresse
        modelBuilder.Entity<Prestataire>()
            .HasOne(p => p.Adresse)
            .WithMany(a => a.Prestataires)
            .HasForeignKey(p => p.AdresseId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation Prestataire - Service
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Prestataire)
            .WithMany(p => p.Services)
            .HasForeignKey(s => s.PrestataireId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Service - Reservation
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Reservation)
            .WithMany(r => r.Services)
            .HasForeignKey(s => s.ReservationId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation Evaluation - Client
        modelBuilder.Entity<Evaluation>()
            .HasOne(e => e.Client)
            .WithMany(c => c.Evaluations)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Evaluation - Service
        modelBuilder.Entity<Evaluation>()
            .HasOne(e => e.Service)
            .WithMany(s => s.Evaluations)
            .HasForeignKey(e => e.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Reservation - Client
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation Reservation - Prestataire
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Prestataire)
            .WithMany(p => p.Reservations)
            .HasForeignKey(r => r.PrestataireId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation Paiement - Service
        modelBuilder.Entity<Paiement>()
            .HasOne(p => p.Service)
            .WithMany(s => s.Paiements)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Notification
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Service)
            .WithMany(s => s.Notifications)
            .HasForeignKey(n => n.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Paiement)
            .WithMany(p => p.Notifications)
            .HasForeignKey(n => n.PaiementId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Reservation)
            .WithMany(r => r.Notifications)
            .HasForeignKey(n => n.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Prestataire - Specialite (n-n)
        modelBuilder.Entity<Prestataire>()
            .HasMany(p => p.Specialites)
            .WithMany(s => s.Prestataires)
            .UsingEntity<Dictionary<string, object>>(
                "PrestataireSpecialite",
                j => j.HasOne<Specialite>().WithMany().HasForeignKey("SpecialiteId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Prestataire>().WithMany().HasForeignKey("PrestataireId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey("PrestataireId", "SpecialiteId")
            );
    }
}
