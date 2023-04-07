using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.entities;

namespace WebApplication1.ViewModels
{
    public class BillViewModels
    {
        public int ID { get; set; }
        public string CreatedTime { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public int PaymentId { get; set; }
        public string PaymentCode { get; set; }
        public int AccId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }

        public BillViewModels() { }

        public BillViewModels(Bill bill)
        {
            this.ID = bill.ID;
            this.CreatedTime = bill.CreatedTime;
            this.Total = bill.Total;
            this.Status = bill.Status;
            this.PaymentId = bill.PaymentId;
            this.PaymentCode = bill.PaymentCode;
            this.AccId = bill.AccId;
        }
    }
}
