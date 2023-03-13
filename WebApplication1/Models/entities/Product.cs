using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.ModelPattern;
using WebApplication1.ViewModels;

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

        [Column("p_image_base64", TypeName = "ntext")]
        public string ImageBase64 { get; set; }

        ////Category
        [Column("cate_id", TypeName = "int")]
        [Required(ErrorMessage = "You must choose category")]
        [ForeignKey("cate_id")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Product()
        {

        }
        public Product(ProductViewModels productView)
        {
            this.ID = productView.ProductID;
            this.Name = productView.ProductName;
            this.Description = productView.ProductDescription;
            this.Status = productView.ProductStatus;
            this.Price = productView.ProductPrice; 
            this.Image = productView.ProductImage is null
                ? FacadeMaker.Instance.GetProductById(productView.ProductID).Image
                : productView.ProductImage;
            this.ImageBase64 = productView.ProductImageBase64 is null
                ? FacadeMaker.Instance.GetProductById(productView.ProductID).ImageBase64
                : productView.ProductImageBase64;
            this.CategoryId = productView.CateID;
            this.Category = FacadeMaker.Instance.GetCategoryById(CategoryId);
        }

    }
}
