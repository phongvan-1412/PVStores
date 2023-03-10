using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.entities
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("bid_id", TypeName = "int", Order = 1)]
        public int ID { get; set; }

        [Column("bid_quantity", TypeName = "int")]
        public int Quantity { get; set; }

        [Column("bid_payment", TypeName = "decimal")]
        public decimal Total { get; set; }

        [Key]
        [ForeignKey("ProductId")]
        [Column("p_id", TypeName = "int", Order = 4)]
        public int ProductID { get; set; }

        [Key]
        [ForeignKey("BillId")]
        [Column("b_id", TypeName = "int", Order = 5)]
        public int BillID { get; set; }


        //Bill
        public Bill Bill { get; set; }

        //Product
        public Product Product { get; set; }

        public BillDetail()
        {

        }
    }
}
