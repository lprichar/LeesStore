using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeesStore.Products
{
    public class CreateProductDto : IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, 999)]
        public decimal Price { get; set; }

        public ProductType ProductType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (ProductType == ProductType.Other && Quantity > 10)
            {
                yield return new ValidationResult(
                    "Quantity must be under 10 for products of type other",
                    new[] { nameof(Quantity) }
                );
            }
        }

    }
}