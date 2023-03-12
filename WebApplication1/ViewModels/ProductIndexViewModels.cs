using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class ProductIndexViewModels
    {
        public List<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public ConvertEnum productStatus;

        public string PageTitle { get; set; }

        public ProductIndexViewModels() { }
    }
}
