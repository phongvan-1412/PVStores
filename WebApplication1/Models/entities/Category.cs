using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models.entities

{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cate_id", TypeName = "int")]
        public int ID { get; set; }

        [Column("cate_name", TypeName = "nvarchar")]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 255 chars")]
        public string Name { get; set; }

        [Column("cate_status", TypeName = "bit")]
        [Display(Name = "Status")]
        public bool Status { get; set; }

        [Column("sub_cate", TypeName = "int")]
        [Display(Name = "Sub Category")]
        [Required(ErrorMessage = "Sub Cate is required")]
        public int SubCate { get; set; }

        //Relationships
        public List<Product> Products { get; set; }
    }
}
