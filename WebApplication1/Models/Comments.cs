using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Comments
    {
        [Key]
        public int cmt_id { get; set; }
        public string cmt_content { get; set; }
        public int cmt_count { get; set; }
        public int like_count { get; set; }
        public int dislike_count { get; set; }
        public int rep_cmt_id { get; set; }

        //Product
        public int p_id { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        //Account
        public int acc_id { get; set; }
        [ForeignKey("AccId")]
        public Account Account { get; set; }

        public Comments()
        {

        }


    }
}
