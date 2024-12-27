namespace WebApplication1.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Montant { get; set; }
        public string Statut { get; set; } // En attente, Validée, En préparation, En livraison, Livrée
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public virtual Livraison Livraison { get; set; }
        public ICollection<Detail> detailsCommande { get; set; }
        public virtual Paiement Paiement { get; set; }
    }
}
