namespace WebApplication1.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
        public int ProduitId { get; set; }
        public ICollection<Produit> Produits { get; set; }
        public int QuantiteCommande { get; set; } // qtecmd
        public decimal Montant { get; set; } // Montant total pour ce produit (QuantiteCommande * PrixUnitaire)
    }
}
