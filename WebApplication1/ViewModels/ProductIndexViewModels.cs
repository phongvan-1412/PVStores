using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.ViewModels
{
    public class ProductIndexViewModels
    {
        private string productStatus;
        public List<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string ConvertEnum(int status)
        {
            switch (status)
            {
                case 0:
                    productStatus = EnumStatus.Inactive.ToString();
                    break;
                case 1:
                    productStatus = EnumStatus.Active.ToString();
                    break;
            }
            return productStatus;
        }

        public string PageTitle { get; set; }

        public ProductIndexViewModels() { }
    }
}
