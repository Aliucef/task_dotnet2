namespace task_dotnet.Models.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
