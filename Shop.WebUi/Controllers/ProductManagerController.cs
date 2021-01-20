using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAccess.InMemory;
using Shop.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.WebUi.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> contextCategory;

        public ProductManagerController()
        {
            //context = new inMemoryRepository<Product>();
            context = new SQLRepository<Product>(new MyContext());
            contextCategory = new SQLRepository<ProductCategory>(new MyContext());
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()//on parle sur la page creation qui vas recevoir un formaulaire de création d un produit
        {
            ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
            viewModel.Product = new Product();

            viewModel.ProductCategories = contextCategory.Collection();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase image) {
            if (!ModelState.IsValid)
            {
                return View(product); //RESTER SUR LA M2ME PAGE AVEC LES MESSAGE ERREUR QUI VA AFFICHER
            }
            else
            {
                if(image!= null)
                {
                    product.Image = product.Name + Path.GetExtension(image.FileName);
                    image.SaveAs(Server.MapPath("~/Content/ProdImages/") + product.Image);
                }
                context.Insert(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }


        }

        public ActionResult Edit(int id)
        {
            Product p = context.FindById(id);
            try
            {
                if (p == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ProductCategoryViewModel viewModel = new ProductCategoryViewModel();
                    viewModel.Product = p;
                    viewModel.ProductCategories = contextCategory.Collection();
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, int id, HttpPostedFileBase image)
        {
            try
            {
                Product prodToEdit = context.FindById(id);
                if(prodToEdit == null)
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
                        if(image != null)
                        {
                            product.Image = product.Name + Path.GetExtension(image.FileName);
                            image.SaveAs(Server.MapPath("~/Content/ProdImages/") + product.Image);
                        }
                        //context.Update(product); ce n'est pas un context entity framework
                        //prodToEdit.Name = product.Name;
                        //prodToEdit.Description = product.Description;
                        //prodToEdit.Category = product.Category;
                        //prodToEdit.Price = product.Price;
                        //prodToEdit.Image = product.Image;
                        context.Update(product);
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
                Product p = context.FindById(id);
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
                Product prodToDelete = context.FindById(id);
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