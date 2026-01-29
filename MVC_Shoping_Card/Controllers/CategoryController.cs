using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_Shoping_Card.Models;
using Shoping_Card_DB_Connection.DataAccess;
using Shoping_Card_DB_Connection.Models;
using X.PagedList;
using X.PagedList.Extensions;


namespace MVC_Shoping_Card.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ISqlData _db;

        public CategoryController(ISqlData db)
        {
            _db = db;
        }

        // GET: CategoryController
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index(string? name, int? page)
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            if (String.IsNullOrEmpty(name))
            {
                categories = await _db.GetAllCategories();
            }
            else
            {
                categories = await _db.SearchCategory(name);
            }

            List<CategoryViewModel> categoriesView = new List<CategoryViewModel>();
            for (int i = 0; i < categories.Count; i++)
            {
                CategoryViewModel cat = new CategoryViewModel()
                {
                    Id = categories[i].Id,
                    Name = categories[i].Name,
                };
                categoriesView.Add(cat);
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var pagedCategories = categoriesView.ToPagedList(pageNumber, pageSize);

            ViewBag.Name = name;

            return View(pagedCategories);
        }

        // GET: CategoryController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dublicate = await _db.GetCategoryByName(model.Name);

            if (dublicate.Count > 0)
            {
                ModelState.AddModelError("Name", "This category name already exists.");
                return View(model);
            }

            _db.CreateCategory(model.Name);

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var category = (await _db.GetCategoryById(id)).FirstOrDefault();
            CategoryViewModel categoryView = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
            };

            return View(categoryView);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dublicate = await _db.GetCategoryByName(model.Name);

            if (dublicate.Count > 1)
            {
                ModelState.AddModelError("Name", "This category name already exists.");
                return View(model);
            }

            CategoryModel category = new CategoryModel()
            {
                Id = model.Id,
                Name = model.Name,
            };

            _db.EditCategory(category);

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = (await _db.GetCategoryById(id)).FirstOrDefault();
            CategoryViewModel categoryview = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(categoryview);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var category = (await _db.GetCategoryById(id)).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            _db.DeleteCategory(category.Id);
            return RedirectToAction(nameof(Index));


        }
    }
}
