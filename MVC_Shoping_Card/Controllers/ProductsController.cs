using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_Shoping_Card.Models;
using Shoping_Card_DB_Connection.DataAccess;
using Shoping_Card_DB_Connection.Models;
using System.Security.Claims;
using X.PagedList;
using X.PagedList.Extensions;

namespace MVC_Shoping_Card.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ISqlData _db;

        public ProductsController(ISqlData db)
        {
            _db = db;
        }

        public ActionResult Index(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            List<ProductModel> products = new List<ProductModel>();

            if (String.IsNullOrEmpty(name) && categoryId == null &&  minPrice == null && maxPrice == null)
            {
                products = _db.GetProducts();
            }
            else
            {
                products = _db.SearchProducts(name, categoryId ?? 0, minPrice ?? 0, maxPrice ?? 0);
            }

            List<ProductViewModel> productsView = new List<ProductViewModel>();
            for (int i = 0; i < products.Count; i++)
            {
                ProductViewModel product = new ProductViewModel();
                product.Id = products[i].Id;
                product.Category = _db.GetCategoryById(products[i].CategoryId).FirstOrDefault().Name;
                product.Name = products[i].Name;
                product.Amount = products[i].Amount;
                product.Info = products[i].Info;
                product.Price = products[i].Price;
                productsView.Add(product);
            }

            // Pass the filters back to the view so they stay after submitting
            ViewBag.Name = name;
            ViewBag.CategoryId = categoryId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Categories = _db.GetAllCategories();

            var pagedProducts = productsView.ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);
        }

        public ActionResult Product(int id)
        {
            var product = _db.GetProductById(id).FirstOrDefault();

            ProductViewModel productView = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Amount = product.Amount,
                Info = product.Info,
                Price = product.Price,
                Category = _db.GetCategoryById(product.CategoryId).FirstOrDefault().Name,
            };

            return View(productView);
        }

        [Authorize(Roles = "Admin")]
        // GET: ProducsController/Create
        public ActionResult Create()
        {
            var categories = _db.GetAllCategories();

            List<CategoryViewModel> categoriesView = new List<CategoryViewModel>();
            for (int i = 0; i < categories.Count; i++)
            {
                CategoryViewModel category = new CategoryViewModel()
                {
                    Id = categories[i].Id,
                    Name = categories[i].Name,
                };
                categoriesView.Add(category);
            }

            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel()
            {
                Categories = categoriesView,
            };

            return View(productCreateViewModel);
        }

        // POST: ProducsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dublicate = _db.GetProductByName(model.Name);

            if (dublicate.Count > 0)
            {
                ModelState.AddModelError("Name", "This Product Is Already Exists");
                return View(model);
            }

            ProductModel product = new ProductModel()
            {
                Name = model.Name,
                Amount = model.Amount,
                Info = model.Info,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            _db.CreateProduct(product);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: ProducsController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _db.GetProductById(id).FirstOrDefault();
            var categories = _db.GetAllCategories();

            List<CategoryViewModel> categoriesView = new List<CategoryViewModel>();
            for (int i = 0; i < categories.Count; i++)
            {
                CategoryViewModel category = new CategoryViewModel()
                {
                    Id = categories[i].Id,
                    Name = categories[i].Name,
                };

                categoriesView.Add(category);
            }

            ProductCreateViewModel productEdit = new ProductCreateViewModel()
            {
                Categories = categoriesView,
                Id = product.Id,
                Name = product.Name,
                Amount = product.Amount,
                Info = product.Info,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };

            return View(productEdit);
        }

        // POST: ProducsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ProductCreateViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var dublicate = _db.GetProductByName(model.Name);
            if(dublicate.Count > 1)
            {
                ModelState.AddModelError("Name", "This Product Is Already Exists");
                return View(model); 
            }

            ProductModel saveProduct = new ProductModel()
            {
                Id = model.Id,
                Name = model.Name,
                Amount = model.Amount,
                Info = model.Info,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            _db.EditProduct(saveProduct);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddToCart(int  id)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var purchase = _db.GetPurchase(userId, id).FirstOrDefault();
            
            var product = _db.GetProductById(id).FirstOrDefault();

            if(purchase == null)
            {
                _db.CreatePurchase(userId, id);
                return RedirectToAction("Product", new { id = id });
            }

            if(purchase.Amount >= product.Amount)
            {
                ModelState.AddModelError($"{product.Name}", $"There is only {product.Amount} number of this product");
                return RedirectToAction("Product", new { id = id });
            }

            _db.UpdatePurchase(purchase.Id);
            return RedirectToAction("Product", new { id = id });

        }

        [Authorize(Roles = "Admin")]
        // GET: ProducsController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = _db.GetProductById(id).FirstOrDefault();

            ProductViewModel productView = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Amount = product.Amount,
                Info = product.Info,
                Price = product.Price,
                Category = _db.GetCategoryById(product.CategoryId).FirstOrDefault().Name
            };

            return View(productView);
        }

        // POST: ProducsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var product = _db.GetProductById(id);
            
            if(product == null)
            {
                return NotFound();
            }

            _db.DeleteProduct(id);
            return RedirectToAction("Index");   
        }
    }
}
