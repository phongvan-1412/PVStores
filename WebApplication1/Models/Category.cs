using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int cate_id { get; set; }

        [Display(Name = "Name")]
        public string cate_name { get; set; }

        [Display(Name = "Status")]
        public bool cate_status { get; set; }

        [Display(Name = "Sub Category")]
        public int sub_cate { get; set; }

        //Relationships
        public List<Product> Products { get; set;}
    }
}
