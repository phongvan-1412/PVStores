using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.entities;

namespace WebApplication1.Models.ModelPattern
{
    public class CategoryMgr : IFacade<Category>
    {
        private readonly PVStoresContext _context;

        public CategoryMgr(PVStoresContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return category;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
