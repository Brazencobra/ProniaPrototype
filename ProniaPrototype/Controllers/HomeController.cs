using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;

namespace ProniaPrototype.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Opinions = _context.Opinions.ToList();
            ViewBag.ShippingAreas = _context.ShippingAreas.ToList();
            ViewBag.Sliders = _context.Sliders.ToList();
            ViewBag.Banners = _context.Banners.ToList();
            return View();
        }
        public IActionResult Shop()
        {
            ViewBag.Categories = _context.Categories.ToList();  
            ViewBag.Colors  = _context.Colors.ToList();
            return View();
        }
    }
}
