using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductDotnet.Models
{
    [Table("Categories", Schema = "master")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "CategoryName length not exceed than 50")]
        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public string? Photo { get; set; }
    }
}