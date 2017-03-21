using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace AzureTableProxy
{
    public class AzureTableClient
    {
        public string StorageAccount { get; set; }
        public string AccessKey { get; set; }
        public string TableName { get; set; }

        public void Add(ContactData contactData)
        {
            var resource = string.Format(@"{0}", TableName);
            var uri = GetHost() + resource;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            AddHeaders(client, request, resource);
            request.Content = new StringContent(JsonConvert.SerializeObject(contactData), Encoding.UTF8,
                "application/json");
            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Add Failed " + content);
            }
        }

        public ContactData Get(int id)
        {
            return InternalGet(id).FirstOrDefault();
        }

        public IEnumerable<ContactData> Get()
        {
            return InternalGet(null);
        }

        public void Update(int id, ContactData contactData)
        {
            var resource = string.Format(@"{0}(PartitionKey='1',RowKey='{1}')", TableName, id.ToString());
            var uri = GetHost() + resource;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            AddHeaders(client, request, resource);
            request.Content = new StringContent(JsonConvert.SerializeObject(contactData), Encoding.UTF8,
                "application/json");
            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Update Failed " + content);
            }
        }

        public void Delete(int id)
        {
            var resource = string.Format(@"{0}(PartitionKey='1',RowKey='{1}')", TableName, id.ToString());
            var uri = GetHost() + resource;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            AddHeaders(client, request, resource);
            request.Headers.Add("If-Match", "*");
            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Update Failed " + content);
            }
        }

        private IEnumerable<ContactData> InternalGet(int? id)
        {
            var resource = string.Format(@"{0}()", TableName);
            if (id != null)
                resource += string.Format(@"?$filter=(ID%20eq%20{0})", id);
            var uri = GetHost() + resource;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            AddHeaders(client, request, resource);
            var response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception("Get Failed");
            var content = response.Content.ReadAsStringAsync().Result;
            var tableValues = (TableValues) JsonConvert.DeserializeObject(content, typeof(TableValues));
            return tableValues.Value;
        }

        internal class TableValues
        {
            public List<ContactData> Value { get; set; }
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static string GetAuthString(string storageAccount, string accessKey, string resourcePath, string str)
        {
            // Signature string for  Shared Key Lite Authentication must be in the form
            // StringToSign = Date + "\n" + CanonicalizedResource
            // Date 
            var stringToSign = str + "\n";

            // Canonicalized Resource in the format  /{0}/{1} where 0 is name of the account and 1 is resources URI path
            // remove the query string
            var query = resourcePath.IndexOf("?", StringComparison.Ordinal);
            if (query > 0)
                resourcePath = resourcePath.Substring(0, query);
            stringToSign += "/" + storageAccount + "/" + resourcePath;

            // Hash-based Message Authentication Code (HMAC) using SHA256 hash
            var hasher = new HMACSHA256(Convert.FromBase64String(accessKey));

            // Authorization header
            var strAuthorization = "SharedKeyLite " + storageAccount + ":" +
                                   Convert.ToBase64String(
                                       hasher.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            return strAuthorization;
        }

        private void AddHeaders(HttpClient client, HttpRequestMessage request, string resource)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=nometadata");
            var str = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);
            request.Headers.Add("x-ms-date", str);
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "3.0;NetFx");
            var strAuthorization = GetAuthString(StorageAccount, AccessKey, resource, str);
            request.Headers.Add("Authorization", strAuthorization);
        }

        private string GetHost() => string.Format(@"https://{0}.table.core.windows.net/", StorageAccount);
    }
}
