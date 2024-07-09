using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_dotnet.Data;
using task_dotnet.Models;
using task_dotnet.Models.Entities;

namespace task_dotnet.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel viewModel)
        {
            var category = new Category
            {
                CategoryName = viewModel.CategoryName,
             
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("List", "Categories");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var category = await dbContext.Categories.ToListAsync();

            return View(category);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Categories.FindAsync(id);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category viewModel)
        {
            var category = await dbContext.Categories.FindAsync(viewModel.Id);
            if (category is not null)
            {
                category.CategoryName = viewModel.CategoryName;
              

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Categories");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category viewModel)
        {
            var category = await dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (category is not null)
            {
                dbContext.Categories.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Categories");

        }
    }
}
