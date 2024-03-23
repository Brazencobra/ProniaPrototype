using Microsoft.AspNetCore.Mvc;
using ProniaPrototype.DAL;
using ProniaPrototype.Models;
using ProniaPrototype.ViewModels;

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
        public IActionResult Create(CreateOpinionVM opinionVM)
        {
            if(!ModelState.IsValid) return View(opinionVM);
            IFormFile? file = opinionVM.Image;
            if (file is null || !file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image","Bu fayl sekl formatinda deyil");
                return View(opinionVM);
            }
            string filename = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\user\\Desktop\\Projects\\ProniaPrototype\\ProniaPrototype\\wwwroot\\assets\\images\\" + filename , FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Opinion opinion = new Opinion 
            { 
                Name = opinionVM.Name , 
                Surname = opinionVM.Surname , 
                Role = opinionVM.Role , 
                Description = opinionVM.Description , 
                ImageUrl = filename
            };
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
