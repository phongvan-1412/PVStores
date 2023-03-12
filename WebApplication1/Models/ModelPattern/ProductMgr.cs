﻿using WebApplication1.Models.entities;

namespace WebApplication1.Models.ModelPattern
{
    public class ProductMgr : IFacade<Product>
    {
        public Product Create(Product product)
        {
            PVStoresContext context = new PVStoresContext();
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
            return context.Products.FirstOrDefault(p => p.ID == id);
        }


    }
}
