using WebApplication1.Models;

namespace WebApplication1.ModelPattern.Services
{
    public abstract class AService<T>
    {
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(int id);
        public abstract void Add(T category);
        public abstract void Update(int id, T newCategory);
        public abstract void Delete(int id);
    }
}
