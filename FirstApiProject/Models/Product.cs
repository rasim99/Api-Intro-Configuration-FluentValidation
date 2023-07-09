namespace FirstApiProject.Models
{
    public class Product
    {
         public int Id { get; set; }
         public string Name { get; set; }
         public double SalePrice { get; set; }
         public double CostPrice { get; set; }
         public bool IsDeleted { get; set; }

         public int? CategoryId { get; set; }
         public Category Category { get; set; }

        //public DateTime CreatedDate { get; set; }

    }
}
