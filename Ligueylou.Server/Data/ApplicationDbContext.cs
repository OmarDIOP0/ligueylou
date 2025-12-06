namespace Ligueylou.Server.Data;

using Ligueylou.Server.Models;
using Ligueylou.Server.Models.abstracts;
using Ligueylou.Server.Models.Enums;
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

        // Table Per Hierarchy (TPH) configuration
        modelBuilder.Entity<Utilisateur>()
            .ToTable("Utilisateurs")
            .HasDiscriminator<RoleEnum>("Role")
            .HasValue<Administrateur>(RoleEnum.ADMIN)
            .HasValue<Client>(RoleEnum.CLIENT)
            .HasValue<Prestataire>(RoleEnum.PRESTATAIRE);
        
        //Indexer Email
        modelBuilder.Entity<Utilisateur>()
            .HasIndex(u => u.Email)
            .IsUnique();
        //indexer Telephone
        modelBuilder.Entity<Utilisateur>()
            .HasIndex(u => u.Telephone)
            .IsUnique();

        // Utilisateur - Adresse
        modelBuilder.Entity<Utilisateur>()
            .HasOne(u => u.Adresse)
            .WithMany(a => a.Utilisateurs)
            .HasForeignKey(u => u.AdresseId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configurer Favori avec clé composite
        modelBuilder.Entity<Favori>()
            .HasKey(f => new { f.ClientId, f.PrestataireId });

        modelBuilder.Entity<Favori>()
            .HasOne(f => f.Client)
            .WithMany(c => c.Favoris)
            .HasForeignKey(f => f.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Favori>()
            .HasOne(f => f.Prestataire)
            .WithMany(p => p.Favoris)
            .HasForeignKey(f => f.PrestataireId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Prestataire - Service
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Prestataire)
            .WithMany(p => p.Services)
            .HasForeignKey(s => s.PrestataireId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Service - Reservation
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Reservation)
            .WithMany(r => r.Services)
            .HasForeignKey(s => s.ReservationId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Evaluation - Client
        modelBuilder.Entity<Evaluation>()
            .HasOne(e => e.Client)
            .WithMany(c => c.Evaluations)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Evaluation - Service
        modelBuilder.Entity<Evaluation>()
            .HasOne(e => e.Service)
            .WithMany(s => s.Evaluations)
            .HasForeignKey(e => e.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Reservation - Client
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Reservation - Prestataire
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Prestataire)
            .WithMany(p => p.Reservations)
            .HasForeignKey(r => r.PrestataireId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Paiement - Service
        modelBuilder.Entity<Paiement>()
            .HasOne(p => p.Service)
            .WithMany(s => s.Paiements)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Notification
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Service)
            .WithMany(s => s.Notifications)
            .HasForeignKey(n => n.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Paiement)
            .WithMany(p => p.Notifications)
            .HasForeignKey(n => n.PaiementId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Reservation)
            .WithMany(r => r.Notifications)
            .HasForeignKey(n => n.ReservationId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relation Prestataire - Specialite (n-n)
        modelBuilder.Entity<Prestataire>()
            .HasMany(p => p.Specialites)
            .WithMany(s => s.Prestataires)
            .UsingEntity<Dictionary<string, object>>(
                "PrestataireSpecialite",
                j => j.HasOne<Specialite>().WithMany().HasForeignKey("SpecialiteId").OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<Prestataire>().WithMany().HasForeignKey("PrestataireId").OnDelete(DeleteBehavior.NoAction),
                j => j.HasKey("PrestataireId", "SpecialiteId")
            );

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}
