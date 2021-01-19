using Shop.Core.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Shop.DataAccess.Sql
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyContext")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Product>Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }

}