using WebApplication1.Models.entities;
using WebApplication1.ViewModels;
using WebApplication1.Utilities;
namespace WebApplication1.Models.ModelPattern
{
    public class BillMgr : IFacade<Bill>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BillMgr() { }

        public Bill Create(Bill bill)
        {
            PVStoresContext context = new PVStoresContext();
            context.Bills.Add(bill);
            context.SaveChanges();

            List<BillDetailViewModels> lstBillDetailView = WebApplication1.Controllers.CartController.billDetail;

            foreach (var item in lstBillDetailView)
            {
                BillDetail billDetail = new BillDetail
                {
                    Quantity = item.Quantity,
                    Total = item.Total,
                    ProductID = item.ProductID,
                    BillID = bill.ID
                };

                FacadeMaker.Instance.CreateBillDetail(billDetail);
            }
            return bill;
        }

        public Bill Update(int id, Bill bill)
        {
            PVStoresContext context = new PVStoresContext();
            context.Bills.Update(bill);
            context.SaveChanges();
            return bill;
        }

        public List<Bill> GetAll()
        {
            PVStoresContext context = new PVStoresContext();
            return context.Bills.ToList();
        }

        public Bill GetById(int id)
        {
            PVStoresContext context = new PVStoresContext();
            return context.Bills.FirstOrDefault(c => c.ID == id);
        }


    }
}
