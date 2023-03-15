using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class ProductViewModels
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        public string ImageBase64 { get; set; }
        public int CategoryId { get; set; }
        public string CateName { get; set; }

        public ProductViewModels() { }
        public ProductViewModels(Product product)
        {
            this.ID = product.ID;
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price;
            this.Status = product.Status;
            this.Image = product.Image;
            this.ImageBase64 = product.ImageBase64;
            this.CategoryId = product.CategoryId;
            this.CateName = FacadeMaker.Instance.GetCategoryById(product.CategoryId).Name;
        }
    }
}
