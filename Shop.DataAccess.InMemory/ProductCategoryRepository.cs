using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCategories;

        public ProductCategoryRepository()
        {
            //bonne pratique
            productsCategories = cache["ProductCategory"] as List<ProductCategory>; //as sert a faire un cast
            if (productsCategories == null)
            {
                productsCategories = new List<ProductCategory>();
            }


        }
        public void SaveChanges()
        {
            cache["productCategories"] = productsCategories;
        }

        public void Inser(ProductCategory p)
        {
            productsCategories.Add(p);
        }
        public void Update(ProductCategory p)
        {
            ProductCategory prodToUpdate = productsCategories.Find(prod => prod.Id == p.Id);
            if (prodToUpdate != null)
            {
                prodToUpdate = p;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory FindById(int id)
        {
            ProductCategory p = productsCategories.Find(prod => prod.Id == id);

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
        public IQueryable<ProductCategory> Collection()
        {
            return productsCategories.AsQueryable();
        }

        public void Delete(int id)
        {
            ProductCategory ProdToDelete = productsCategories.Find(p => p.Id == id);
            if (ProdToDelete != null)
            {
                productsCategories.Remove(ProdToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }


    }
}

