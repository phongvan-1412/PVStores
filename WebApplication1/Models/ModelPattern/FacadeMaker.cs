using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.Models.ModelPattern
{
    public sealed class FacadeMaker
    {
        private static FacadeMaker _instance = null;
        private CategoryMgr _categoryMgr;
        private ProductMgr _productMgr;

        private FacadeMaker()
        {
            _categoryMgr = new CategoryMgr();
            _productMgr = new ProductMgr(); 
        }

        public static FacadeMaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FacadeMaker();
                }
                return _instance;
            }
        }

        //Category
        public Category CreateCategory(Category category)
        {
            _categoryMgr.Create(category);
            return category;
        }
        public Category UpdateCategory(int id, Category category)
        {
            _categoryMgr.Update(id, category);
            return category;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryMgr.GetAll();
        }
        public Category GetCategoryById(int id)
        {
            return _categoryMgr.GetById(id);
        }

        //Product
        public Product CreateProduct(Product product)
        {
            _productMgr.Create(product);
            return product;
        }
        public Product UpdateProduct(int id, Product product)
        {
            _productMgr.Update(id, product);
            return product;
        }
        public List<Product> GetAllProducts()
        {
            return _productMgr.GetAll();
        }
        public Product GetProductById(int id)
        {
            return _productMgr.GetById(id);
        }
    }
}
