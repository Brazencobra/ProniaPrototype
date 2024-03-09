using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ColorController : Controller
    {
        readonly AppDbContext _context;
        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Colors.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid) return View();
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Color existcolor = _context.Colors.Find(id);
            if (existcolor is null) return NotFound();
            return View(existcolor);
        }
        [HttpPost]
        public IActionResult Update(int? id,Color color) 
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            Color existcolor = _context.Colors.Find(id);
            if (existcolor is null) return NotFound();
            existcolor.Name = color.Name;
            existcolor.Count = color.Count;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Color existcolor = _context.Colors.Find(id);
            if(existcolor is null) return NotFound();
            _context.Colors.Remove(existcolor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
