namespace WebApplication1.Models
{
    public class Paiement
    {
        public int Id { get; set; }
        public string Type { get; set; } // OM, Wave, Chèque
        public decimal Montant { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
    }
}
