using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int cate_id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 255 chars")]
        public string cate_name { get; set; }

        [Display(Name = "Status")]
        public bool cate_status { get; set; }

        [Display(Name = "Sub Category")]
        [Required(ErrorMessage = "Sub Cate is required")]
        public int sub_cate { get; set; }

        //Relationships
        public List<Product> Products { get; set; }
    }
}
