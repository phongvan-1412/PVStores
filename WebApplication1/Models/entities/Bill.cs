using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.entities
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("b_id", TypeName = "int")]
        public int ID { get; set; }

        [Column("created_time", TypeName = "varchar")]
        public string CreatedTime { get; set; }

        [Column("b_total", TypeName = "decimal")]
        public decimal Total { get; set; }

        [Column("b_status", TypeName = "int")]
        public int Status { get; set; }

        [Column("payment_id", TypeName = "int")]
        public int PaymentId { get; set; }

        [Column("payment_code", TypeName = "varchar")]
        public int PaymentCode { get; set; }


        //Account
        [Column("acc_id", TypeName = "int")]
        [ForeignKey("AccId")]
        public int AccId { get; set; }
        public Account Account { get; set; }

        //Relationships
        public List<BillDetail> BillDetail { get; set; }

        public Bill()
        {

        }
    }
}
