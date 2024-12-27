using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.services
{
    public interface ICommandeService
    {
        Task<IEnumerable<Commande>> GetAllCommandes();
        Task<Commande> GetCommandeById(int? id);
        Task<bool> CreateCommande(Commande commande, List<Detail> details);
        Task<bool> UpdateCommande(Commande commande, List<Detail> details);
        Task<bool> DeleteCommande(int id);
        bool CommandeExists(int id);
        Task<IEnumerable<SelectListItem>> GetClientsSelectList(int? selectedClientId = null);
        Task<IEnumerable<SelectListItem>> GetProduitsSelectList();
        Task<List<Detail>> GetCommandeDetails(int commandeId);
    }
}
