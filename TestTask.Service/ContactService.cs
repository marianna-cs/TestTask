using TestTask.Data;
using TestTask.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Service
{
    public class ContactService : Repository<Contact>
    {
        public ContactService(BaseContext context) : base(context)
        {               
        }

        public Contact GetContactByEmail(string email)
        {
            return _context.Contacts.FirstOrDefault(e => e.Email == email);
        }
        public bool UniquenessVerification(string email)
        {
            var contact = _context.Contacts.FirstOrDefault(a => a.Email == email);
            if (contact!=null && contact.Email.Equals(email))
            {
                return false;
            }
            return true;
        }
    }
}
