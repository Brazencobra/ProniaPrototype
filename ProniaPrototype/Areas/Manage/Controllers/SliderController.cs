using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;
using ProniaPrototype.ViewModels;

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
        public IActionResult Create(CreateSliderVM sliderVM)
        {
            if(!ModelState.IsValid) return View();
            if(_context.Sliders.Any(x=>x.Order == sliderVM.Order))
            {
                ModelState.AddModelError("Order",$"{sliderVM.Order} reqeme sahib order artiq var");
                return View();
            }
            IFormFile? file = sliderVM.Image;
            if (file is null || !file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Bu fayl sekil formatinda deyil");
                return View();
            }
            string filename = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\user\\Desktop\\Projects\\ProniaPrototype\\ProniaPrototype\\wwwroot\\assets\\images\\" + filename , FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Slider slider = new Slider 
            {
                Name = sliderVM.Name,
                DiscountPercent = sliderVM.DiscountPercent,
                Description = sliderVM.Description,
                Order = sliderVM.Order,
                ImageUrl = filename
            };
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
        public IActionResult Update(int? id, CreateSliderVM sliderVM)
        {
            if(!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            Slider existslider = _context.Sliders.Find(id);
            if (existslider is null) return NotFound();
            existslider.Name = sliderVM.Name;
            existslider.DiscountPercent = sliderVM.DiscountPercent;
            existslider.Description = sliderVM.Description;
            IFormFile file = sliderVM.Image;
            string filename = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\user\\Desktop\\Projects\\ProniaPrototype\\ProniaPrototype\\wwwroot\\assets\\images\\" + filename , FileMode.Create))
            {
                file.CopyTo(stream);
            }
            existslider.ImageUrl = filename;
            if (_context.Sliders.Any(s=>s.Order == sliderVM.Order))
            {
                Slider firstslider = _context.Sliders.FirstOrDefault(x=>x.Order == sliderVM.Order);
                firstslider.Order = existslider.Order;
                existslider.Order = sliderVM.Order;
            }
            else 
            { 
                existslider.Order = sliderVM.Order;
            }
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
