using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Client> Client { get; set; } = default!;
        public DbSet<WebApplication1.Models.Commande> Commande { get; set; } = default!;
        public object Produits { get; internal set; }
        public IEnumerable<object> Produit { get; internal set; }
        public object Detail { get; internal set; }
    }
}
