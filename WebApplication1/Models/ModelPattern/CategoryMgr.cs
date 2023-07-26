using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.entities;

namespace WebApplication1.Models.ModelPattern
{
    public class CategoryMgr : IFacade<Category>
    {
        public CategoryMgr()
        {
        }

        public Category Create(Category category)
        {
            PVStoresContext context = new PVStoresContext();
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }
        public Category Update(int id, Category category)
        {
            PVStoresContext context = new PVStoresContext();
            context.Categories.Update(category);
            context.SaveChanges();
            return category;
        }

        public List<Category> GetAll()
        {
            PVStoresContext context = new PVStoresContext();
            return context.Categories.Where(c => c.Status==true).ToList();
        }
        public List<Category> GetAllCategories()
        {
            PVStoresContext context = new PVStoresContext();
            return context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            PVStoresContext context = new PVStoresContext();
            return context.Categories.FirstOrDefault(c => (c.ID == id && c.Status==true));
        }
    }
}
