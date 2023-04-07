using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.ViewModels;

namespace WebApplication1.Models.entities
{
    [Table("BillDetail")]
    public class BillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("bid_id", TypeName = "int")]
        public int ID { get; set; }

        [Column("bid_quantity", TypeName = "int")]
        public int Quantity { get; set; }

        [Column("bid_payment", TypeName = "decimal")]
        public decimal Total { get; set; }

        [Column("p_id", TypeName = "int")]
        public int ProductID { get; set; }

        [Column("b_id", TypeName = "int")]
        public int BillID { get; set; }


        public BillDetail() { }
        public BillDetail(BillDetailViewModels billDetailView)
        {
            this.Quantity = billDetailView.Quantity;
            this.Total = billDetailView.Total;
            this.ProductID = billDetailView.ProductID;
            this.BillID = billDetailView.BillID;
        }

    }
}
