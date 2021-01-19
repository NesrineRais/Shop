using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class ProductCategoryController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryController()
        {
            context = new inMemoryRepository<ProductCategory>(); //si le client demain change la base de donner
            //on met a jour juste cette ligne 
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()//on parle sur la page creation qui vas recevoir un formaulaire de création d un produit
        {
            ProductCategory p = new ProductCategory();
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product); //RESTER SUR LA M2ME PAGE AVEC LES MESSAGE ERREUR QUI VA AFFICHER
            }
            else
            {
                context.Insert(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }


        }

        public ActionResult Edit(int id)
        {
            ProductCategory p = context.FindById(id);
            try
            {
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory product, int id)
        {
            try
            {
                ProductCategory prodToEdit = context.FindById(id);
                if (prodToEdit == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View(product);
                    }
                    else
                    {
                        //context.Update(product); ce n'est pas un context entity framework
                        prodToEdit.Category = product.Category;
                       
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

            }
            catch
            {
                return HttpNotFound();
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                ProductCategory p = context.FindById(id);
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(p);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirDelete(int id)
        {
            try
            {
                ProductCategory prodToDelete = context.FindById(id);
                if (prodToDelete == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    context.Delete(id);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }


        }

    }
}