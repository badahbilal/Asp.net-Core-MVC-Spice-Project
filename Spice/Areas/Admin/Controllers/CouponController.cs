using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Coupon Coupon { get; set; }

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Coupon.ToListAsync());
        }


        //Get - Create 
        public IActionResult Create()
        {
            return View();
        }

        //Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] picture1 = null;

                    // in this up comming lines we handle the proccess to seve image in database
                    // as a bute value
                    using (var fileStrem1 = files[0].OpenReadStream())
                    {
                        using var memoryStream1 = new MemoryStream();
                        fileStrem1.CopyTo(memoryStream1);
                        picture1 = memoryStream1.ToArray();
                    }
                    Coupon.Picture = picture1;
                }
                _db.Coupon.Add(Coupon);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(Coupon);
        }

        // Get - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coupon = await _db.Coupon.FindAsync(id);
            if (Coupon == null)
            {
                return NotFound();
            }
            return View(Coupon);
        }


        // Psot - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var couponFromDb = await _db.Coupon.FindAsync(id);
            var couponFromDb = await _db.Coupon.Where(c => c.id == Coupon.id).FirstOrDefaultAsync();

            if (couponFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] picture1 = null;

                    // in this up comming lines we handle the proccess to seve image in database
                    // as a bute value
                    using (var fileStrem1 = files[0].OpenReadStream())
                    {
                        using var memoryStream1 = new MemoryStream();
                        fileStrem1.CopyTo(memoryStream1);
                        picture1 = memoryStream1.ToArray();
                    }
                    couponFromDb.Picture = picture1;
                }

                couponFromDb.Name = Coupon.Name;
                couponFromDb.CouponType = Coupon.CouponType;
                couponFromDb.Discount = Coupon.Discount;
                couponFromDb.MinimumAmount = Coupon.MinimumAmount;
                couponFromDb.isActive = Coupon.isActive;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(Coupon);
        }


        // Get - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coupon = await _db.Coupon.FindAsync(id);
            if (Coupon == null)
            {
                return NotFound();
            }
            return View(Coupon);
        }


        // Get - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coupon = await _db.Coupon.FindAsync(id);
            if (Coupon == null)
            {
                return NotFound();
            }
            return View(Coupon);
        }

        // Post - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeletePost(int? id)
        {
            // Check if id null or not
            if (id == null)
            {
                return NotFound();
            }

            //var couponFromDb = await _db.Coupon.FindAsync(id);
            var couponFromDb = await _db.Coupon.SingleOrDefaultAsync(m => m.id == id);

            if (couponFromDb == null)
            {
                return NotFound();
            }
            _db.Coupon.Remove(couponFromDb);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
