using TestTask.Data;

namespace TestTask.Controllers.Entities
{
    public class EntitiesProducer
    {
        public Contact UpdateContactFromRequest(Contact contact, Account account, IncidentCreationRequest request)
        {
            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.Email = request.Email;
            contact.AccountId = account.Id;
            return contact;
        }

        public Contact CreateContactFromRequest(Account account, IncidentCreationRequest request)
        {
            Contact contact = new Contact()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                AccountId = account.Id
            };
            return contact;
        }



    }
}
