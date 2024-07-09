namespace task_dotnet.Models.Entities
{


    public class SubCategory
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<SubSubCategory> SubSubCategories { get; set; } = new List<SubSubCategory>();
    }
}