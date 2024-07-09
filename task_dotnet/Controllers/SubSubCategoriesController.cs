using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_dotnet.Data;
using task_dotnet.Models.Entities;
using System;
using System.Threading.Tasks;

namespace task_dotnet.Controllers
{
    public class SubSubCategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubSubCategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.SubCategories = await dbContext.SubCategories.ToListAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(SubSubCategory viewModel)
        {
            
                var subSubCategory = new SubSubCategory
                {
                    SubSubCategoryName = viewModel.SubSubCategoryName,
                    SubCategoryId = viewModel.SubCategoryId
                };
                await dbContext.SubSubCategories.AddAsync(subSubCategory);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            

         
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var subSubCategories = await dbContext.SubSubCategories
                                                .Include(s => s.SubCategory)
                                                .ThenInclude(sc => sc.Category)
                                                .ToListAsync();
            return View(subSubCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var subSubCategory = await dbContext.SubSubCategories.FindAsync(id);
            if (subSubCategory == null)
            {
                return NotFound("SubSubCategory not found");
            }

            ViewBag.SubCategories = await dbContext.SubCategories.ToListAsync();
            return View(subSubCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubSubCategory subSubCategory)
        {
            
                var existingSubSubCategory = await dbContext.SubSubCategories.FindAsync(subSubCategory.Id);
                if (existingSubSubCategory != null)
                {
                    existingSubSubCategory.SubSubCategoryName = subSubCategory.SubSubCategoryName;
                    existingSubSubCategory.SubCategoryId = subSubCategory.SubCategoryId;
                    await dbContext.SaveChangesAsync();
                }
                return RedirectToAction("List","SubSubCategories");
            

           
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var subSubCategory = await dbContext.SubSubCategories.FindAsync(id);
            if (subSubCategory != null)
            {
                dbContext.SubSubCategories.Remove(subSubCategory);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return NotFound("SubSubCategory not found");
        }
    }
}
