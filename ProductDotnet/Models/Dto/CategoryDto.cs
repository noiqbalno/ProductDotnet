using System.ComponentModel.DataAnnotations;

namespace ProductDotnet.Models.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "CategoryName length not exceed than 50")]
        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public string? Photo { get; set; }

    }
}