using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using task_dotnet.Data;
using task_dotnet.Models;

namespace task_dotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories
                                     .Include(c => c.SubCategories)
                                         .ThenInclude(sc => sc.SubSubCategories)
                                     .Select(c => new CategoryViewModel
                                     {
                                         Id = c.Id,
                                         Name = c.CategoryName,
                                         SubCategories = c.SubCategories.Select(sc => new SubCategoryViewModel
                                         {
                                             Id = sc.Id,
                                             Name = sc.SubCategoryName,
                                             SubSubCategories = sc.SubSubCategories.Select(ssc => new SubSubCategoryViewModel
                                             {
                                                 Id = ssc.Id,
                                                 Name = ssc.SubSubCategoryName
                                             }).ToList()
                                         }).ToList()
                                     }).ToList();

            return View(categories);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
