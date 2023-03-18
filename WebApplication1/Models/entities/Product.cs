using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.ModelPattern;
using WebApplication1.ViewModels;
using System.Web;

namespace WebApplication1.Models.entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("p_id", TypeName = "int")]
        public int ID { get; set; }

        [Column("p_name", TypeName = "nvarchar")]
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }

        [Column("p_description", TypeName = "nvarchar")]
        public string Description { get; set; }

        [Column("p_price", TypeName = "decimal")]
        [Required(ErrorMessage = "Product Price is required")]
        public decimal Price { get; set; }

        [Column("p_status", TypeName = "bit")]
        public bool Status { get; set; }

        [Column("p_image", TypeName = "varchar")]
        public string Image { get; set; }

        ////Category
        [Column("cate_id", TypeName = "int")]
        [Required(ErrorMessage = "You must choose category")]
        public int CategoryId { get; set; }

        public Product()
        {

        }
        public Product(ProductViewModels productView)
        {
            this.Name = productView.Name;
            this.Description = productView.Description;
            this.Status = productView.Status;
            this.Price = productView.Price;
            this.CategoryId = productView.CategoryId;
            this.Image = productView.Image;
        }
    }
}
