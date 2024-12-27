namespace WebApplication1.Models
{
    public class Livreur
    {
        public int LivreurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public virtual ICollection<Livraison> Livraisons { get; set; }

        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}
