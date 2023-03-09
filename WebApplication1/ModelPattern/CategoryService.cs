using Microsoft.EntityFrameworkCore;
using WebApplication1.ModelPattern.Services;
using WebApplication1.Models;

namespace WebApplication1.ModelPattern
{
    public class CategoryService : AService<Category>
    {

        public override void Add(Category category)
        {
            var _context = new PVStoresContext();
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Category> GetAll()
        {
            var _context = new PVStoresContext();
            return _context.Category.ToList();
        }

        public override Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(int id, Category newCategory)
        {
            throw new NotImplementedException();
        }
    }
}
