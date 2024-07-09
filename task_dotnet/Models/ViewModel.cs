
    namespace task_dotnet.Models
    {
        public class CategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<SubCategoryViewModel> SubCategories { get; set; } = new List<SubCategoryViewModel>();
        }

        public class SubCategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<SubSubCategoryViewModel> SubSubCategories { get; set; } = new List<SubSubCategoryViewModel>();
        }

        public class SubSubCategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }


