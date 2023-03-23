using WebApplication1.Models.entities;

namespace WebApplication1.Models.ModelPattern
{
    public class AccountMgr : IFacade<Account>
    {
        public AccountMgr() { }

        public Account Create(Account account)
        {
            PVStoresContext context = new PVStoresContext();
            context.Accounts.Add(account);
            context.SaveChanges();
            return account;
        }

        public Account Update(int id, Account account)
        {
            PVStoresContext context = new PVStoresContext();
            context.Accounts.Update(account);
            context.SaveChanges();
            return account;
        }

        public List<Account> GetAll()
        {
            PVStoresContext context = new PVStoresContext();
            return context.Accounts.ToList();
        }

        public Account GetById(int id)
        {
            PVStoresContext context = new PVStoresContext();
            return context.Accounts.FirstOrDefault(a => a.ID == id);
        }

        
    }
}
