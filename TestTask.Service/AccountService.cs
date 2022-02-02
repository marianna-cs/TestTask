using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data;
using TestTask.Repo;

namespace TestTask.Service
{
    public class AccountService : Repository<Account>
    {
        public AccountService(BaseContext context) : base(context)
        {
        }
        public void BuildAccount(Account account, Contact contact)
        {
            account.Contacts = new List<Contact>() { contact };
        }
        public Account GetAccountByName(string accountName)
        {
            return _context.Accounts.FirstOrDefault(a=>a.Name == accountName);
        }
        public bool UniquenessVerification(string accountName)
        {
           var account =  _context.Accounts.FirstOrDefault(a=>a.Name == accountName);
            if (account!=null && account.Name.Equals(accountName))
            {
                return false;
            }
            return true;
        }
    }
}
