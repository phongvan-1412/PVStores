using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        [Key]
        public int p_id { get; set; }
        public string p_name { get; set; }
        public string p_description { get; set; }
        public string imported_at { get; set; }
        public int p_imported_quantity { get; set; }
        public string exported_at { get; set; }
        public int p_exported_quantity { get; set; }
        public int cmt_count { get; set; }
        public int like_count { get; set; }
        public int dislike_count { get; set; }
        public int p_status { get; set; }
        public string p_thumbnail { get; set; }
        public string p_thumbnail_base64 { get; set; }
        public string p_img1 { get; set; }
        public string p_img1_base64 { get; set; }
        public string p_img2 { get; set; }
        public string p_img2_base64 { get; set; }
        public string p_img3 { get; set; }
        public string p_img3_base64 { get; set; }
        public string p_img4 { get; set; }
        public string p_img4_base64 { get; set; }
        public string p_img5 { get; set; }
        public string p_img5_base64 { get; set; }

        //Category
        public int cate_id { get; set; }
        [ForeignKey("CateId")]
        public Category Category { get; set; }

        //Account
        public int acc_id { get; set; }
        [ForeignKey("AccId")]
        public Account Account { get; set; }

        //Producer
        public int pro_id { get; set; }
        [ForeignKey("ProId")]
        public Producer Producer { get; set; }  

        public Product()
        {

        } 

    }
}
