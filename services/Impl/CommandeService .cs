using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Data.Entity;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.services.Impl
{
    public class CommandeService : ICommandeService
    {
        private readonly WebApplication1Context _context;

        public CommandeService(WebApplication1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Commande>> GetAllCommandes()
        {
            return await _context.Commande
                .Include(c => c.Client)
                .Include(c => c.detailsCommande)
                    .ThenInclude(d => d.Produits)
                .ToListAsync();
        }

        public async Task<Commande> GetCommandeById(int? id) => await _context.Commande
                .Include(c => c.Client)
                .Include(c => c.detailsCommande)
                    .ThenInclude(d => d.Produits)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> CreateCommande(Commande commande, List<Detail> details)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Calcul du montant total de la commande
                commande.Montant = details.Sum(d => d.Montant);
                commande.Date = DateTime.Now;
                commande.Statut = "En attente";

                // Ajout de la commande
                _context.Add(commande);
                await _context.SaveChangesAsync();

                // Ajout des détails
                foreach (var detail in details)
                {
                    detail.CommandeId = commande.Id;
                    _context.Detail.Add(detail);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateCommande(Commande commande, List<Detail> details)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Suppression des anciens détails
                var existingDetails = await _context.Detail
                    .Where(d => d.CommandeId == commande.Id)
                    .ToListAsync();
                _context.Detail.RemoveRange(existingDetails);

                // Mise à jour du montant total
                commande.Montant = details.Sum(d => d.Montant);

                // Mise à jour de la commande
                _context.Update(commande);

                // Ajout des nouveaux détails
                foreach (var detail in details)
                {
                    detail.CommandeId = commande.Id;
                    _context.Detail.Add(detail);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<List<Detail>> GetCommandeDetails(int commandeId)
        {
            return await _context.
                .Include(d => d.Produits)
                .Where(d => d.CommandeId == commandeId)
                .ToListAsync();
        }

        // ... autres méthodes existantes ...

        public async Task<IEnumerable<SelectListItem>> GetProduitsSelectList()
        {
            return await _context.Produits
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Nom} - {p.Prix:C}"
                })
                .ToListAsync();
        }

        Task<IEnumerable<Commande>> ICommandeService.GetAllCommandes()
        {
            throw new NotImplementedException();
        }

        Task<Commande> ICommandeService.GetCommandeById(int? id)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICommandeService.CreateCommande(Commande commande, List<Detail> details)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICommandeService.UpdateCommande(Commande commande, List<Detail> details)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICommandeService.DeleteCommande(int id)
        {
            throw new NotImplementedException();
        }

        bool ICommandeService.CommandeExists(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SelectListItem>> ICommandeService.GetClientsSelectList(int? selectedClientId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SelectListItem>> ICommandeService.GetProduitsSelectList()
        {
            throw new NotImplementedException();
        }

        Task<List<Detail>> ICommandeService.GetCommandeDetails(int commandeId)
        {
            throw new NotImplementedException();
        }
    }
}
