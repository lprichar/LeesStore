using System.ComponentModel.DataAnnotations;

namespace LeesStore.Products
{
    public class CreateProductDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }
}