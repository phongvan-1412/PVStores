using WebApplication1.Models.entities;

namespace WebApplication1.Models.ModelPattern
{
    public class BillDetailMgr
    {
        public BillDetailMgr()
        {
        }

        public BillDetail Create(BillDetail billDetail)
        {
            PVStoresContext context = new PVStoresContext();
            context.BillDetails.Add(billDetail);
            context.SaveChanges();
            return billDetail;
        }
        public BillDetail Update(int id, BillDetail billDetail)
        {
            PVStoresContext context = new PVStoresContext();
            context.BillDetails.Update(billDetail);
            context.SaveChanges();
            return billDetail;
        }

        public List<BillDetail> GetAll()
        {
            PVStoresContext context = new PVStoresContext();
            return context.BillDetails.ToList();
        }

        public BillDetail GetById(int id)
        {
            PVStoresContext context = new PVStoresContext();
            return context.BillDetails.FirstOrDefault(c => c.ProductID == id);
        }
    }
}
