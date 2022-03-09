using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;
using System.Linq;

namespace Restaurant.UI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public AboutController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_context.Abouts.FirstOrDefault());
        }
        public IActionResult Update(int id)
        {
            About dbAbout = _context.Abouts.Where(x => x.Id == id).FirstOrDefault();
            if (dbAbout is null) return NotFound();
            AboutUpdateVM about = _mapper.Map<AboutUpdateVM>(dbAbout);
            return View(about);
        }
    }
}
