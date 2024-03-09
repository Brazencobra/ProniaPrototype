using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;
using System.Diagnostics;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Category existcategory = _context.Categories.Find(id);
            if(existcategory is null) return NotFound();
            return View(existcategory);
        }
        [HttpPost]
        public IActionResult Update(int? id,Category category) 
        {
            if (id is null) return BadRequest();
            Category existcategory = _context.Categories.Find(id);
            if (existcategory is null) return NotFound();
            existcategory.Name = category.Name;
            existcategory.Count = category.Count;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id) 
        {
            if (id is null) return BadRequest();
            Category category = _context.Categories.Find(id);
            if (category is null) return NotFound();
            _context.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));        
        }
    }
}
