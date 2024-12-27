﻿using System.ComponentModel.DataAnnotations;
using WebApplication1.Validations;

namespace WebApplication1.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le surnom est obligatoire")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Le surnom doit avoir au moins 5 caractères et inférieur à 20 caractères")]
        [UniqueSurnom(ErrorMessage = "Ce surnom existe déja")]
        public string Surnom { get; set; }
        [Required(ErrorMessage = "Le téléphone est obligatoire")]
        [RegularExpression(@"^(77|78|76)[0-9]{7}$", ErrorMessage = "Le téléphone doit commencer par 77 ou 78 ou 76 et doit avoir au 9 chiffres")]
        public string Telephone { get; set; }
        public string? Adresse { get; set; }

        // Relation
        public User? User { get; set; }
        public int? UserId { get; set; }
        public decimal SoldeCompte { get; set; }
        public ICollection<Commande> Commandes { get; set; }
    }
}
