using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureTableProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TestLibrary
{
    [TestClass]
    public class AzureTableClientTests
    {
        readonly AzureTableClient _azureTableClient = new AzureTableClient { AccessKey = "3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg==", StorageAccount = "samplestorageaccount123", TableName = "TestTable" };

        [TestMethod]
        public void TestAdd()
        {
            _azureTableClient.Add(new ContactData {ID = 10, FirstName = "Dan", LastName = "Bailey", TelNo = "023-456789"});
        }

        [TestMethod]
        public void TestUpdate()
        {
            _azureTableClient.Update(6, new ContactData { ID = 6, FirstName = "Dan", LastName = "BaileyBoy2", TelNo = "023-456789" });
        }

        [TestMethod]
        public void TestDelete()
        {
            _azureTableClient.Delete(6);
        }

        [TestMethod]
        public void TestGet()
        {
            var contacts = _azureTableClient.Get();
        }
    }
}
