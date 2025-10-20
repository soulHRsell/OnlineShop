using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Shoping_Card.Models;
using Shoping_Card_DB_Connection.DataAccess;
using Shoping_Card_DB_Connection.Models;
using System.Security.Claims;
using X.PagedList.Extensions;

namespace MVC_Shoping_Card.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISqlData _db;

        public AccountController(ISqlData db)
        {
            _db = db;
        }

        [Authorize]
        public ActionResult Profile()
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _db.GetUserById(userId).FirstOrDefault();
            var userView = new UserViewModel()
            {
                Username = user.Username,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                State = user.State,
                City = user.City,
                ZipCode = user.ZipCode,
                CardNumber = user.CardNumber,
            };

            return View(userView);
        }

        public ActionResult Cart()
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cartItems = _db.GetUncompletedPurchasesByUserId(userId);

            List<PurchaseViewModel> cartItemsView = new List<PurchaseViewModel>();

            for (int i = 0; i < cartItems.Count; i++)
            {
                var product = _db.GetProductById(cartItems[i].ProductId).FirstOrDefault();

                var category = _db.GetCategoryById(product.CategoryId).FirstOrDefault();

                PurchaseViewModel item = new PurchaseViewModel()
                {
                    Id = cartItems[i].Id,
                    PurchaseDate = cartItems[i].PurchaseDate,
                    UserId = cartItems[i].UserId,

                    Product = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Amount = product.Amount,
                        Info = product.Info,
                        Price = product.Price,

                        Category = new CategoryViewModel()
                        {
                            Id = category.Id,
                            Name = category.Name,
                        }

                    },

                    IsCompleted = cartItems[i].IsCompleted,
                    IsSent = cartItems[i].IsSent,
                    Amount = cartItems[i].Amount,
                };

                cartItemsView.Add(item);    
            }

            return View(cartItemsView);
        }

        [Authorize]
        public ActionResult PurchaseHistory(int? id, string? name, string? status, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<PurchaseModel> purchases = new List<PurchaseModel>();  

            if (String.IsNullOrEmpty(name) && id == null && String.IsNullOrEmpty(status))
            {
                purchases = _db.GetPurchaseByUserId(userId);
            }
            else
            {
                purchases = _db.SearchCompletedAndSentPurchasesByUserId(id ?? 0, name, status, userId);
            }

            List<PurchaseViewModel> purchasesView = new List<PurchaseViewModel>();

            for (int i = 0; i < purchases.Count; i++)
            {
                var product = _db.GetProductById(purchases[i].ProductId).FirstOrDefault();

                var category = _db.GetCategoryById(product.CategoryId).FirstOrDefault();


                PurchaseViewModel item = new PurchaseViewModel()
                {
                    Id = purchases[i].Id,
                    PurchaseDate = purchases[i].PurchaseDate,
                    UserId = purchases[i].UserId,
                    Product = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Amount = product.Amount,
                        Info = product.Info,
                        Price = product.Price,

                        Category = new CategoryViewModel()
                        {
                            Id = category.Id,
                            Name = category.Name,
                        }
                    },

                    IsCompleted = purchases[i].IsCompleted,
                    IsSent = purchases[i].IsSent,
                    Amount = purchases[i].Amount,
                };

                purchasesView.Add(item);
            }

            ViewBag.Id = id;
            ViewBag.Name = name;
            ViewBag.Status = status;

            var pagedPurchases = purchasesView.ToPagedList(pageNumber, pageSize);
            
            return View(pagedPurchases);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManagePurchases(int? id, string? name, string? status, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var purchases = _db.AdminSearchPurchases(id, name, status);

            var purchaseViewList = purchases.Select(p =>
            {
                var product = _db.GetProductById(p.ProductId).FirstOrDefault();
                var category = _db.GetCategoryById(product.CategoryId).FirstOrDefault();
                var user = _db.GetUserById(p.UserId).FirstOrDefault();

                return new PurchaseViewModel
                {
                    Id = p.Id,
                    PurchaseDate = p.PurchaseDate,
                    IsCompleted = p.IsCompleted,
                    IsSent = p.IsSent,
                    Amount = p.Amount,
                    UserId = p.UserId,
                    UserName = user.Username,
                    
                    Product = new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Category = new CategoryViewModel { Id = category.Id, Name = category.Name }
                    }
                };
            }).ToList();

            ViewBag.Id = id;
            ViewBag.Name = name;
            ViewBag.Status = status;

            return View(purchaseViewList.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult MarkAsSent(int id)
        {
            _db.MarkPurchaseAsSent(id); 
            return RedirectToAction("ManagePurchases");
        }

        // GET: AccountController for Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: AccountController/Create for Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dublicate = _db.GetUserByUsername(model.Username);

            if (dublicate.Count > 0)
            {
                ModelState.AddModelError("Username", "This Username already taken.");
                return View(model);
            }

            var user = new UserModel
            {
                Username = model.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                EmailAddress = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Country = model.Country,
                State = model.State,
                City = model.City,
                ZipCode = model.ZipCode,
                CardNumber = model.CardNumber,
            };

            _db.Createuser(user);
            return RedirectToAction(nameof(Login));
        }

        // GET: AccountController for Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Create for Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, bool rememberMe)
        {
            var user = _db.GetUserByUsername(model.Username).FirstOrDefault();

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                };

                await HttpContext.SignInAsync("Cookies", principal, authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        // GET: AccountController/Edit/5
        [Authorize]
        public ActionResult Edit()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserModel dbuser = _db.GetUserById(Int32.Parse(userId)).FirstOrDefault();

            if (dbuser == null)
            {
                return NotFound();
            }

            UserViewModel user = new UserViewModel()
            {
                Username = dbuser.Username,
                EmailAddress = dbuser.EmailAddress,
                FirstName = dbuser.FirstName,
                LastName = dbuser.LastName,
                Country = dbuser.Country,
                State = dbuser.State,
                City = dbuser.City,
                ZipCode = dbuser.ZipCode,
                CardNumber = dbuser.CardNumber,
            };

            return View(user);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var dublicate = _db.GetUserByUsername(model.Username);

            if(dublicate.Count > 1)
            {
                ModelState.AddModelError("Username", "This Username already taken.");
                return View(model); 
            }

            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            UserModel editedUser = new UserModel()
            {
                Username = model.Username,
                EmailAddress = model.EmailAddress,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Country = model.Country,
                State = model.State,
                City = model.City,
                ZipCode = model.ZipCode,
                CardNumber = model.CardNumber,
            };
            

            _db.EditUserInfo(userId, editedUser);
            return RedirectToAction("Profile");

        }

        [Authorize]
        public ActionResult CartItemDelete(int id)
        {
            var purchase = _db.GetPuchaseById(id).FirstOrDefault();

            var product = _db.GetProductById(purchase.ProductId).FirstOrDefault();

            var category = _db.GetCategoryById(product.CategoryId).FirstOrDefault();

            PurchaseViewModel purchaseView = new PurchaseViewModel()
            {
                Id = purchase.Id,
                PurchaseDate = purchase.PurchaseDate,
                UserId = purchase.UserId,

                Product = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Amount = product.Amount,
                    Info = product.Info,
                    Price = product.Price,

                    Category = new CategoryViewModel()
                    {
                        Id = category.Id,
                        Name = category.Name,
                    }

                },

                IsCompleted = purchase.IsCompleted,
                IsSent = purchase.IsSent,
                Amount = purchase.Amount,
            };

            return View(purchaseView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CartItemDeleteConfirm(int id)
        {
            var purchase = _db.GetPuchaseById(id).FirstOrDefault();

            if (purchase == null)
            {
                return NotFound();
            }

            _db.DeletePurchase(id);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var purchase = _db.GetPuchaseById(id).FirstOrDefault();

            if (purchase == null)
            {
                return NotFound();
            }

            _db.UpdatePurchaseQuantity(id, quantity);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(List<int> cartItemsId)
        {
            foreach(var Id in cartItemsId)
            {
                _db.MakePurchaseComplete(Id);
            }

            return Redirect("Cart");
        }

    }
}
