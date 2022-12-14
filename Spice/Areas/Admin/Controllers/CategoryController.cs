using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Utility;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.ManagerUser)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Get - index
        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.ToListAsync());
        }

        //Get - Create
        public IActionResult Create()
        {
            return View();
        }


        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // if Valid
                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        //Get - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        //Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                // if Valid
                _db.Update(category);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }



        //Get - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        //Post - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {

            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //Get - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

    }
}
