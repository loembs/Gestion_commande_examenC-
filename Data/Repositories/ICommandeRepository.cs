using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Data.Repositories
{
    public interface ICommandeRepository
    {
        IQueryable<Commande> GetCommandesDuJour();
        IQueryable<Livraison> GetLivraisonsDuJour();
        IQueryable<Paiement> GetPaiementsDuJour();
        IQueryable<Commande> GetCommandesEnAttente();
        bool ClientADroitRemise(int clientId);
        IQueryable<Paiement> GetHistoriqueVersements(int clientId);
    }
}

