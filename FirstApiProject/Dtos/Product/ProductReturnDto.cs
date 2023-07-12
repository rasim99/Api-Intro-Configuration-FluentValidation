namespace FirstApiProject.Dtos.Product
{
    public class ProductReturnDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public double Profit { get; set; }
        public bool IsDeleted { get; set; }
        public  CategoryInProductReturnDto Category { get; set; }

    }
    public class CategoryInProductReturnDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int ProductsCount { get; set; }

    }
}
