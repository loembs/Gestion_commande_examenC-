using System;
using System.Linq;
using WebApplication1.Models;
 
namespace WebApplication1.Data.Repositories.Impl
{

    public class CommandeRepository : ICommandeRepository
    {
        private readonly CommandeDbContext _context;

        public CommandeRepository(CommandeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Commande> GetCommandesDuJour()
        {
            return _context.Commandes
                .Where(c => c.Date.Date == DateTime.Today);
        }

        public IQueryable<Livraison> GetLivraisonsDuJour()
        {
            return _context.Livraisons
                .Where(l => l.DateLivraison.Date == DateTime.Today);
        }

        public IQueryable<Paiement> GetPaiementsDuJour()
        {
            return _context.Paiements
                .Where(p => p.Date.Date == DateTime.Today);
        }

        public IQueryable<Commande> GetCommandesEnAttente()
        {
            return _context.Commandes
                .Where(c => c.Statut == "EnAttente");
        }

        public bool ClientADroitRemise(int clientId)
        {
            var debutMois = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var finMois = debutMois.AddMonths(1).AddDays(-1);

            var nbCommandes = _context.Commandes
                .Count(c => c.ClientId == clientId
                        && c.Date.Date >= debutMois
                        && c.Date.Date <= finMois);

            return nbCommandes >= 10;
        }

        public IQueryable<Paiement> GetHistoriqueVersements(int clientId)
        {
            return _context.Paiements
                .Where(p => p.Commande.ClientId == clientId)
                .OrderByDescending(p => p.Date);
        }
    }
}


