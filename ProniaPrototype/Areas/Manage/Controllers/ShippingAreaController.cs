using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ShippingAreaController : Controller
    {
        readonly AppDbContext _context;

        public ShippingAreaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ShippingAreas.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ShippingArea shippingArea)
        {
            if (!ModelState.IsValid) return View(shippingArea);
            _context.ShippingAreas.Add(shippingArea);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            ShippingArea existshippingarea = _context.ShippingAreas.Find(id);
            if (existshippingarea is null) return NotFound();
            return View(existshippingarea);
        }
        [HttpPost]
        public IActionResult Update(int? id,ShippingArea shippingarea)
        {
            if (id is null) return BadRequest();
            ShippingArea existshippingarea = _context.ShippingAreas.Find(id);
            if (existshippingarea is null) return NotFound();
            existshippingarea.Name = shippingarea.Name;
            existshippingarea.Description = shippingarea.Description;
            existshippingarea.ImageUrl = shippingarea.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if(id is null) return BadRequest();
            ShippingArea shippingarea = _context.ShippingAreas.Find(id);
            if (shippingarea is null) return NotFound();
            _context.ShippingAreas.Remove(shippingarea);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
