using System.Data.Entity;
using WebApplication1;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CommandeDbContext : DbContext
    {

        public bool? IsLazyLoadingEnabled { get; set; }

        public CommandeDbContext() : base("CommandeDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = IsLazyLoadingEnabled ?? true;
        }

        //public CommandeDbContext() : base("name=CommandeDbConnection") => this.Configuration.LazyLoadingEnabled = true;

        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Livreur> Livreurs { get; set; }
        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuration des relations Client-Commande
            modelBuilder.Entity<Commande>()
                .HasRequired(c => c.Client)
                .WithMany(c => c.Commandes)
                .HasForeignKey(c => c.ClientId)
                .WillCascadeOnDelete(false);

            // Configuration des relations Detail-Commande
            modelBuilder.Entity<Detail>()
                .HasRequired(d => d.Commande)
                .WithMany(c => c.detailsCommande)
                .HasForeignKey(d => d.CommandeId)
                .WillCascadeOnDelete(false);

            // Configuration des relations Detail-Produit
            modelBuilder.Entity<Detail>()
                .HasRequired(d => d.Produit)
                .WithMany(p => p.DetailsCommande)
                .HasForeignKey(d => d.ProduitId)
                .WillCascadeOnDelete(false);

            // Configuration des relations Livraison-Commande
            modelBuilder.Entity<Livraison>()
                .HasRequired(l => l.Commande)
                .WithOptional(c => c.Livraison);

            // Configuration des relations Paiement-Commande
            modelBuilder.Entity<Paiement>()
                .HasRequired(p => p.Commande)
                .WithOptional(c => c.Paiement);
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Client)
                .WithOptionalPrincipal(c => c.User);

            base.OnModelCreating(modelBuilder);// Ajout de l'appel à la méthode de base
        }
    }
}