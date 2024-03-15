using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;
using ProniaPrototype.ViewModels;

namespace ProniaPrototype.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BannerController : Controller
    {
        readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Banners.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBannerVM bannerVM)
        {
            IFormFile? file = bannerVM.Image;
            if (file is null || !file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image","Yuklediyiniz fayl sekl formatinda deyil!");
                return View();
            }
            if (file.Length > 500 * 1024)
            {
                ModelState.AddModelError("Image", $"Yuklediyiniz faylin hecmi 500kb-den coxdur : {((double)file.Length / (1024 * 1024)).ToString().Substring(0,4)}mb");
                return View();
            }
            string filename = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\user\\Desktop\\Projects\\ProniaPrototype\\ProniaPrototype\\wwwroot\\assets\\images\\" + filename , FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Banner banner = new Banner {
                CollectionTitle = bannerVM.CollectionTitle,
                MainTitle = bannerVM.MainTitle,
                ImageUrl = filename
            };
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Banner banner = _context.Banners.Find(id);
            if (banner is null) return NotFound();
            _context.Banners.Remove(banner);
            _context.SaveChanges(true);
            return RedirectToAction(nameof(Index));
        }
    }
}
