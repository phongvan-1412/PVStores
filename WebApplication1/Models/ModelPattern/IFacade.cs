using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.ModelPattern
{
    public interface IFacade<T>
    {
        T Create(T obj);
        T Update(int id, T obj);
        List<T> GetAll();
        T GetById(int id);

    }
}
