using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class CategoryViewModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int SubCate { get; set; }

        public CategoryViewModels() { }
        public CategoryViewModels(Category category) {
            this.ID = category.ID;
            this.Name = category.Name;
            this.Status = category.Status;
            this.SubCate = category.SubCate;
        }

    }
}
