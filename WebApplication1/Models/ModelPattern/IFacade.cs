using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.ModelPattern
{
    public interface IFacade<T>
    {
        T Create(T obj);

        List<T> GetAll();
    }
}
