using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.Models.ModelPattern
{
    public sealed class FacadeMaker
    {
        private static FacadeMaker _instance = null;
        private AccountMgr _accountMgr;
        private CategoryMgr _categoryMgr;
        private ProductMgr _productMgr;
        private BillDetailMgr _billDetailMgr;

        private FacadeMaker()
        {
            _accountMgr = new AccountMgr();
            _categoryMgr = new CategoryMgr();
            _productMgr = new ProductMgr();
            _billDetailMgr = new BillDetailMgr();
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

        //BillDetail
        public BillDetail CreateBillDetail(BillDetail billDetail)
        {
            _billDetailMgr.Create(billDetail);
            return billDetail;
        }
        public BillDetail UpdateBillDetail(int id, BillDetail billDetail)
        {
            return billDetail;
        }
        public List<BillDetail> GetAllBillDetails()
        {
            return _billDetailMgr.GetAll();
        }
        public BillDetail GetBillDetailById(int id)
        {
            return _billDetailMgr.GetById(id);
        }

        //Account
        public Account CreateAccount(Account account)
        {
            _accountMgr.Create(account);
            return account;
        }
        public Account UpdateAccount(int id, Account account)
        {
            _accountMgr.Update(id, account);
            return account;
        }
        public List<Account> GetAllAccounts()
        {
            return _accountMgr.GetAll();
        }
        public Account GetAccountById(int id)
        {
            return _accountMgr.GetById(id);
        }
        public Account GetAccountByEmail(string email)
        {
            return _accountMgr.GetByEmail(email);
        }
    }
}
