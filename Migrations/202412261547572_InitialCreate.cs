namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surnom = c.String(nullable: false, maxLength: 20),
                        Telephone = c.String(nullable: false),
                        Adresse = c.String(),
                        UserId = c.Int(),
                        SoldeCompte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Statut = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommandeId = c.Int(nullable: false),
                        ProduitId = c.Int(nullable: false),
                        QuantiteCommande = c.Int(nullable: false),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commandes", t => t.CommandeId)
                .ForeignKey("dbo.Produits", t => t.ProduitId)
                .Index(t => t.CommandeId)
                .Index(t => t.ProduitId);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        QuantiteStock = c.Int(nullable: false),
                        PrixUnitaire = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantiteSeuil = c.Int(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Livraisons",
                c => new
                    {
                        LivraisonId = c.Int(nullable: false),
                        DateLivraison = c.DateTime(nullable: false),
                        AdresseLivraison = c.String(),
                        CommandeId = c.Int(nullable: false),
                        LivreurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LivraisonId)
                .ForeignKey("dbo.Commandes", t => t.LivraisonId)
                .ForeignKey("dbo.Livreurs", t => t.LivreurId, cascadeDelete: true)
                .Index(t => t.LivraisonId)
                .Index(t => t.LivreurId);
            
            CreateTable(
                "dbo.Livreurs",
                c => new
                    {
                        LivreurId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Telephone = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.LivreurId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 20),
                        Prenom = c.String(nullable: false, maxLength: 40),
                        Login = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 255),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleUsers", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Paiements",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reference = c.String(),
                        Date = c.DateTime(nullable: false),
                        CommandeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commandes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paiements", "Id", "dbo.Commandes");
            DropForeignKey("dbo.Livreurs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.RoleUsers");
            DropForeignKey("dbo.Clients", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Livraisons", "LivreurId", "dbo.Livreurs");
            DropForeignKey("dbo.Livraisons", "LivraisonId", "dbo.Commandes");
            DropForeignKey("dbo.Details", "ProduitId", "dbo.Produits");
            DropForeignKey("dbo.Details", "CommandeId", "dbo.Commandes");
            DropForeignKey("dbo.Commandes", "ClientId", "dbo.Clients");
            DropIndex("dbo.Paiements", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Livreurs", new[] { "UserId" });
            DropIndex("dbo.Livraisons", new[] { "LivreurId" });
            DropIndex("dbo.Livraisons", new[] { "LivraisonId" });
            DropIndex("dbo.Details", new[] { "ProduitId" });
            DropIndex("dbo.Details", new[] { "CommandeId" });
            DropIndex("dbo.Commandes", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "User_Id" });
            DropTable("dbo.Paiements");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Livreurs");
            DropTable("dbo.Livraisons");
            DropTable("dbo.Produits");
            DropTable("dbo.Details");
            DropTable("dbo.Commandes");
            DropTable("dbo.Clients");
        }
    }
}
