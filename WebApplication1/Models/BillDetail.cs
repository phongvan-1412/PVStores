using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class BillDetail
    {
        public int bid_id { get; set; }
        public int bid_amount { get; set; }
        public decimal bid_payment { get; set; }

        //Bill
        public int b_id { get; set; }
        [ForeignKey("BillId")]
        public Bill Bill { get; set; }

        //Product
        public int p_id { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public BillDetail()
        {

        }
    }
}
