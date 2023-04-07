using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;

namespace WebApplication1.ViewModels
{
    public class BillDetailViewModels
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } 
        public string ProductImage { get; set; }
        public int BillID { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }

        public BillDetailViewModels() { }

        public BillDetailViewModels(BillDetail billDetail)
        {
            this.ID= billDetail.ID;
            this.Quantity = billDetail.Quantity;
            this.Total = billDetail.Total;
            this.ProductID = billDetail.ProductID;
            this.BillID= billDetail.BillID;
        }

    }
}
