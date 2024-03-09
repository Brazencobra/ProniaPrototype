using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OpinionController : Controller
    {
        readonly AppDbContext _context;
        public OpinionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Opinions.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Opinion opinion)
        {
            if(!ModelState.IsValid) return View(opinion);
            _context.Opinions.Add(opinion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if(id is null) return BadRequest();
            Opinion existopinion = _context.Opinions.Find(id);
            if(existopinion is null) return NotFound();
            return View(existopinion);
        }
        [HttpPost]
        public IActionResult Update(int? id,Opinion opinion)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            Opinion existopinion = _context.Opinions.Find(id);
            if (existopinion is null) return NotFound();
            existopinion.Name = opinion.Name;
            existopinion.Surname = opinion.Surname;
            existopinion.Role = opinion.Role;
            existopinion.Description = opinion.Description;
            existopinion.ImageUrl = opinion.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if(id is null) return BadRequest();
            Opinion opinion = _context.Opinions.Find(id);
            if (opinion is null) return NotFound();
            _context.Remove(opinion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
