using System.Collections.Generic;
using System.Linq;
using AzureTableProxy;

namespace WebApplication1.Data
{
    /// <summary>
    ///     Mock contact repository
    /// </summary>
    public class MockContactRepo : IContactRepo
    {
        private static readonly Dictionary<int, ContactData> MockContacts = new Dictionary<int, ContactData>();

        static MockContactRepo()
        {
            MockContacts.Add(0,
                new ContactData {ID = 0, FirstName = "David", LastName = "Smith", TelNo = "023-6780045"});
            MockContacts.Add(1,
                new ContactData {ID = 1, FirstName = "Peter", LastName = "Green", TelNo = "087-6780590"});
        }

        public IEnumerable<ContactData> Get()
        {
            return MockContacts.Values;
        }

        public ContactData Get(int id)
        {
            return MockContacts.Values.First(m => m.ID == id);
        }

        public void Add(ContactData contactData)
        {
            contactData.ID = MockContacts.Count > 0 ? MockContacts.Keys.Max() + 1 : 1;
            MockContacts.Add(contactData.ID, contactData);
        }

        public void Update(int id, ContactData contactData)
        {
            var updateContact = MockContacts.Values.First(a => a.ID == id);
            if (updateContact != null && contactData != null)
            {
                updateContact.FirstName = contactData.FirstName;
                updateContact.LastName = contactData.LastName;
                updateContact.TelNo = contactData.TelNo;
            }
        }

        public void Delete(int id)
        {
            var todo = MockContacts.Values.First(a => a.ID == id);
            if (todo != null)
                MockContacts.Remove(todo.ID);
        }
    }
}
