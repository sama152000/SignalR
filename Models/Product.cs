using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductCommentApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Image { get; set; } // هنا هيخزن اسم الملف

        public List<Comment> Comments { get; set; }
    }
}