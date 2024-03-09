using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BrandController : Controller
    {
        public AppDbContext _context { get; }
        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Brands.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid) return View();
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Brand existbrand = _context.Brands.Find(id);
            if (existbrand is null) return NotFound();
            return View(existbrand);
        }
        [HttpPost]
        public IActionResult Update(int? id,Brand brand)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return NotFound();
            Brand existbrand = _context.Brands.Find(id);
            if(existbrand is null) return BadRequest();
            existbrand.ImageUrl = brand.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Brand brand = _context.Brands.Find(id);
            if(brand is null) return NotFound();
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
