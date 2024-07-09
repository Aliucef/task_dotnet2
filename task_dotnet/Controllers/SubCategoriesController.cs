using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_dotnet.Data;
using task_dotnet.Models;
using task_dotnet.Models.Entities;

namespace task_dotnet.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubCategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await dbContext.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubCategory viewModel)
        {
            var subCategory = new SubCategory
            {
                SubCategoryName = viewModel.SubCategoryName,
                CategoryId = viewModel.CategoryId
            };
            await dbContext.SubCategories.AddAsync(subCategory);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "SubCategories");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var subCategories = await dbContext.SubCategories
                                                .Include(s => s.Category)
                                                .ToListAsync();
            return View(subCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var subCategory = await dbContext.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound("empty");
            }
            ViewBag.Categories = await dbContext.Categories.ToListAsync();
            return View(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubCategory subCategory)
        {
            var existingSubCategory = await dbContext.SubCategories.FindAsync(subCategory.Id);
            if (existingSubCategory is not null)
            {
                existingSubCategory.SubCategoryName = subCategory.SubCategoryName;
                existingSubCategory.CategoryId = subCategory.CategoryId;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "SubCategories");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var subCategory = await dbContext.SubCategories.FindAsync(id);
                if (subCategory != null)
                {
                    dbContext.SubCategories.Remove(subCategory);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("List");
                }
                else
                {
                    return NotFound("SubCategory not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here as needed
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
