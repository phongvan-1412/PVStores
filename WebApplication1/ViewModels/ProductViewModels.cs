using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class ProductViewModels
    {
        public int ProductID { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Status")]
        public bool ProductStatus { get; set; }

        [Display(Name = "Image")]
        public string ProductImage { get; set; }

        public string ProductImageBase64 { get; set; }
        public int CateID { get; set; }
        public string CateName { get; set; }

        public ProductViewModels() { }
        public ProductViewModels(Product product)
        {
            this.ProductID = product.ID;
            this.ProductName = product.Name;
            this.ProductDescription = product.Description;
            this.ProductPrice = product.Price;
            this.ProductStatus = product.Status;
            this.ProductImage = product.Image;
            this.ProductImageBase64 = product.ImageBase64;
            this.CateID = product.CategoryId;
            this.CateName = FacadeMaker.Instance.GetCategoryById(CateID).Name;
        }
    }
}
