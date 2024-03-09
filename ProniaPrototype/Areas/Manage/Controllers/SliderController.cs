using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        readonly AppDbContext _context;
        public SliderController(AppDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if(!ModelState.IsValid) return View(slider);
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Slider existslider = _context.Sliders.Find(id);
            if(existslider is null) return NotFound();
            return View(existslider);
        }
        [HttpPost]
        public IActionResult Update(int? id,Slider slider)
        {
            if(!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            Slider existslider = _context.Sliders.Find(id);
            if (existslider is null) return NotFound();
            existslider.Name = slider.Name;
            existslider.DiscountPercent = slider.DiscountPercent;
            existslider.Description = slider.Description;
            existslider.ImageUrl = slider.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
