using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductRepsitory
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepsitory()
        {
            //bonne pratique
            products = cache["products"] as List<Product>; //as sert a faire un cast
            if (products == null)
            {
                products = new List<Product>();
            }


        }
        public void SaveChanges()
        {
            cache["products"] = products;
        }

        public void Inser(Product p)
        {
            products.Add(p);
        }
        public void Update(Product p)
        {
            Product prodToUpdate = products.Find(prod => prod.Id == p.Id);
            if (prodToUpdate != null)
            {
                prodToUpdate = p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public Product FindById(int id)
        {
            Product p = products.Find(prod => prod.Id == id);

            if (p != null)
            {
                return p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        //Le type Iquerable accéte les reqiuéte LINQ contrairement a une LIst classique
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void  Delete(int id)
        {
            Product ProdToDelete= products.Find(p => p.Id == id);
            if(ProdToDelete != null)
            {
                products.Remove(ProdToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }


    }
}
