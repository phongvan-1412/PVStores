using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Name { get; set; }

        [Column("p_description", TypeName = "nvarchar")]
        public string Description { get; set; }

        [Column("p_price", TypeName = "decimal")]
        public decimal Price { get; set; }

        [Column("p_status", TypeName = "int")]
        public int Status { get; set; }

        [Column("p_image", TypeName = "varchar")]
        public string Image { get; set; }

        [Column("p_image_base64", TypeName = "ntext")]
        public string ImageBase64 { get; set; }

        ////Category
        //[ForeignKey("CateId")]
        //[Column("cate_id", TypeName = "int")]
        public int CateId { get; set; }
        public Category Categories { get; set; }

        public int AccId { get; set; }
        public Account Account { get; set; }

        public Product()
        {

        }

    }
}
