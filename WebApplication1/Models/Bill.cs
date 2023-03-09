using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Bill
    {
        [Key]
        public int b_id { get; set; }
        public string created_time { get; set; }
        public decimal b_total { get; set; }
        public int b_status { get; set; }

        //Account
        public int acc_id { get; set; }
        [ForeignKey("AccId")]
        public Account Account { get; set; }

        //Relationships
        public List<BillDetail> BillDetail { get; set; }

        public Bill()
        {

        }
    }
}
