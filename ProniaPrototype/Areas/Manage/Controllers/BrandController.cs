using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;
using ProniaPrototype.ViewModels;

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
        public IActionResult Create(CreateBrandVM brandVM)
        {
            if (!ModelState.IsValid) return View(brandVM);
            IFormFile? file = brandVM.Image;
            if (file is null || !file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image","Bu fayl sekl formatinda deyil");
                return View(brandVM);
            }
            string filename = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\user\\Desktop\\Projects\\ProniaPrototype\\ProniaPrototype\\wwwroot\\assets\\images\\" + filename, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Brand brand = new Brand 
            {
                ImageUrl = filename
            };
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
