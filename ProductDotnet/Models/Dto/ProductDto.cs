using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductDotnet.Models.Base;

namespace ProductDotnet.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string? Photo { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
