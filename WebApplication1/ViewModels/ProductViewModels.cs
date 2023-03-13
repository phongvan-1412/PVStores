using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class ProductViewModels
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductStatus { get; set; }
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
            this.ProductImageBase64 = product.Image;
            this.CateID = product.CategoryId;
            this.CateName = FacadeMaker.Instance.GetCategoryById(CateID).Name;
        }
    }
}
