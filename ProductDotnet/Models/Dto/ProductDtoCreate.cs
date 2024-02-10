using System.ComponentModel.DataAnnotations;

namespace ProductDotnet.Models.Dto
{
        public class ProductDtoCreate
        {
            public int Id { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "ProductName length not exceed than 50")]
            public string ProductName { get; set; }

            public decimal Price { get; set; }
            public int Stock { get; set; }


            [Required(ErrorMessage = "Please select image")]
            public IFormFile Photo { get; set; }
            public int CategoryId { get; set; }

            public Category? Category { get; set; }
    }
}
