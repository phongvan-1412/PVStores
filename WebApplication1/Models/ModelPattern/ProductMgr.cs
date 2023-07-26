using WebApplication1.Models.entities;

namespace WebApplication1.Models.ModelPattern
{
    public class ProductMgr : IFacade<Product>
    {
        public ProductMgr() { }
        public Product Create(Product product)
        {
            PVStoresContext context = new PVStoresContext();
            product.Status = true;
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }
        public Product Update(int id, Product product)
        {
            PVStoresContext context = new PVStoresContext();
            context.Products.Update(product);
            context.SaveChanges();
            return product;
        }

        public List<Product> GetAll()
        {
            PVStoresContext context = new PVStoresContext();
            return context.Products.ToList();
        }

        public Product GetById(int id)
        {
            PVStoresContext context = new PVStoresContext();
            return context.Products.Where(p => p.ID == id).FirstOrDefault();
        }

        public List<Product> GetProductByCateId(int id)
        {
            PVStoresContext context = new PVStoresContext();
            return context.Products.Where(p => p.CategoryId == id).ToList();
        }
    }
}
