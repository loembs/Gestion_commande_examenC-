namespace WebApplication1.Models
{
    public class Livraison
    {
        public int LivraisonId { get; set; }
        public DateTime DateLivraison { get; set; }
        public string AdresseLivraison { get; set; }
        public int CommandeId { get; set; }
        public int LivreurId { get; set; }
        public virtual Commande Commande { get; set; }
        public virtual Livreur Livreur { get; set; }
    }

}
