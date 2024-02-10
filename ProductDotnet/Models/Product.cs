using ProductDotnet.Models.Base;
using ProductDotnet.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductDotnet.Models;

namespace ProductDotnet.Models
{
    [Table("Products", Schema = "master")]
    public class Product : IIdentityModel
    {
        [Key]
        [Column("ProductID")]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Range(EntityConstantModel.MIN_PRICE, EntityConstantModel.MAX_PRICE)]
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string? Photo { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}