using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        //constructor: get implementation
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //_db.Categories.ToList() convert records in Category  to list， if not list, no need
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET action method
        public IActionResult Create()
        {
            
            return View();
        }
        //POST
        [HttpPost]
        //protected Data from attack
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if(ModelState.IsValid)
            {
                //Add data locally
                _db.Categories.Add(obj);
                //Push to db
                _db.SaveChanges();

                //store data changing b4 redirect
                TempData["success"] = "Category created successfully";
                //RedirectToAction("PageName", "Controller");
                return RedirectToAction("Index");
            }       
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //find base on PK
            var categoryFromDb = _db.Categories.Find(id);
            // u is generic obj
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.ID == id);
            //Single: throw exception when no that id
            //Default: not when not found
            //first or default: throw first element when not found
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.ID == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            // find: jump to that page
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				//POST: use update method instead of add
				_db.Categories.Update(obj);
				//Push to db
				_db.SaveChanges();
				TempData["success"] = "Category edited successfully";
				//RedirectToAction("PageName", "Controller");
				return RedirectToAction("Index");
			}
			return View(obj);
		}
        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //find base on PK
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            // find: jump to that page
            return View(categoryFromDb);
        }

		// POST
		//Add action name: if asp-action is changed its name to Delete, can still use this method
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // ? in C#: nullable variable
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");  
        }
    }
}
