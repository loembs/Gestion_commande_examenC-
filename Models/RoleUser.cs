namespace WebApplication1.Models
{
    public class RoleUser
    {
        public int Id { get; set; }
        public string Nom { get; set; } // Exemple : "Secrétaire", "Comptable", "RS"
        public ICollection<User> Users { get; set; }
    }
}
