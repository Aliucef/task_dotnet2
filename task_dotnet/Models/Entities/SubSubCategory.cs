namespace task_dotnet.Models.Entities

{


    public class SubSubCategory
    {
        public Guid Id { get; set; }
        public string SubSubCategoryName { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }

}