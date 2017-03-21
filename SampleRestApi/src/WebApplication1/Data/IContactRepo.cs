using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureTableProxy;

namespace WebApplication1.Data
{
    /// <summary>
    /// Interface for data repository
    /// </summary>
    public interface IContactRepo
    {
        IEnumerable<ContactData> Get();
        ContactData Get(int id);
        void Add(ContactData contactData);
        void Update(int id, ContactData contactData);
        void Delete(int id);
    }
}
