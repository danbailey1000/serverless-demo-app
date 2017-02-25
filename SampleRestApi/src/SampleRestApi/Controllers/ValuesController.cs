using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleRestApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            String json;
            //3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg
            // https://samplestorageaccount123.table.core.windows.net/TestTable
            AzureTableHelper.RequestResource("samplestorageaccount123",
                "3lCJBMsiu5pF3JmZ994HBYy4o2nu6Zb7cM4YfGUnv1DBPg9hCy76kmSp8waBo1KuUORebTjXGH54NwW9pZ3Wmg==", "TestTable",
                out json);
            return new String[] { json };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
           
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    
}
