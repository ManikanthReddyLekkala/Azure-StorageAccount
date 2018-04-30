using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Newtonsoft.Json;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;


namespace DocumentDB
{
    
    class Program
    {
        private const string account = "https://manikanthsql.documents.azure.com:443/";
        private const string key = "tUAuC0WezYJrsUpqk9Y3HtR3RuX4H4Ja29iHhQqgHVfDRJfa1vRGP4H6VVIHfbRNOVTEWjilawtVHNsakTC13w==";
        private DocumentClient client;
        
        static void Main(string[] args)
        {
            TestDocDb().Wait();
            
        }

        private static async Task TestDocDb()
        {
            Program p = new Program
            {
                client = new DocumentClient(new Uri(account), key)
            };
            string id = "SalesDB";
            var database = p.client.CreateDatabaseQuery().Where(db => db.Id == id).AsEnumerable().FirstOrDefault();
            if (database == null)
            {
                database = await p.client.CreateDatabaseAsync(new Database { Id = id });
            }
            
            string collectionName = "Customers";
            var collection = p.client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(c=>c.Id == collectionName).AsEnumerable().FirstOrDefault();
            if (collection==null)
            {
                collection = await p.client.CreateDocumentCollectionAsync(database.CollectionsLink,
                    new DocumentCollection {Id = collectionName});
            }
            var contoso = new Customer
            {
                CustomerName = "Contoso Corp",
                PhoneNumber = new PhoneNumber[]
                {
                    new PhoneNumber
                    {
                        CountryCode = "1",
                        AreaCode = "619",
                        MainNumber = "555-1212"
                    },
                    new PhoneNumber
                    {
                        CountryCode = "1",
                        AreaCode = "760",
                        MainNumber = "555-2442"
                    }
                }
            };
            await p.client.CreateDocumentAsync(collection.DocumentsLink, contoso);
        }
    }
}
