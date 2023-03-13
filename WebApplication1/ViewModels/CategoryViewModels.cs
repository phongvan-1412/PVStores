using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class CategoryViewModels
    {
        public int CateId { get; set; }
        public string CateName { get; set; }
        public bool CateStatus { get; set; }
        public int SubCate { get; set; }

        public CategoryViewModels() { }
        public CategoryViewModels(Category category) {
            this.CateId = category.ID;
            this.CateName = category.Name;
            this.CateStatus = category.Status;
            this.SubCate = category.SubCate;
        }

    }
}
