using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.entities
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cmt_id", TypeName = "int")]
        public int ID { get; set; }

        [Column("cmt_content", TypeName = "nvarchar")]
        public string Content { get; set; }

        //Account
        [Column("acc_id", TypeName = "int")]
        public int AccID { get; set; }

        [Column("cmt_status", TypeName = "bit")]
        public bool Status { get; set; }

        public Feedback()
        {

        }


    }
}
