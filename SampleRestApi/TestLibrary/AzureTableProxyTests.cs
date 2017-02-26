using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureTableProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLibrary
{
    [TestClass]
    public class AzureTableProxyTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            String json;
            ////3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg
            //// https://samplestorageaccount123.table.core.windows.net/TestTable
            TableHelper.RequestResource("samplestorageaccount123",
                "3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg==", "TestTable",
               out json);
            Console.WriteLine(json);
        }

        [TestMethod]
        public void TestInsert()
        {
           // String json;
            ////3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg
            //// https://samplestorageaccount123.table.core.windows.net/TestTable
            TableHelper.Insert("samplestorageaccount123",
                "3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg==", "TestTable",
               "{\"PartitionKey\":\"5\",\"RowKey\":\"8\"}");
           // Console.WriteLine(json);
        }

        [TestMethod]
        public void TestMethod2()
        {
           
            Console.WriteLine("eererer");
            System.Diagnostics.Debug.WriteLine("Matrix has you...");
        }
    }
}
