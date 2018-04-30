using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace StorageAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            //var blockBlob = container.GetBlockBlobReference("img2.png");
            //using (var fileStream = System.IO.File.OpenRead(@"c:\Azure Storage\img2.png"))
            //{
            //    blockBlob.UploadFromStream(fileStream);
            //}

            //var blobs = container.ListBlobs();
            //foreach (var item in blobs)
            //{
            //    Console.WriteLine(item.Uri);
            //}

            //var blockBlob = container.GetBlockBlobReference("img2.png");
            //using (var fileStream = System.IO.File.OpenWrite(@"c:\Azure Storage\img5.png"))
            //{
            //    blockBlob.DownloadToStream(fileStream);
            //}

            //var blockBlob = container.GetBlockBlobReference("img2.png");
            //var blockBlobCopy = container.GetBlockBlobReference("img3.png");
            //var cb = new AsyncCallback(x => Console.WriteLine("blob copy completed"));
            //blockBlobCopy.BeginStartCopy(blockBlob.Uri, cb, null);

            //SetMetaData(container);
            
            //GetMetaData(container);
            
            
            Console.Read();

        }

        static void SetMetaData(CloudBlobContainer container)
        {
            container.Metadata.Clear();
            container.Metadata.Add("Owner","Mike");
            container.Metadata["Updated"] = DateTime.Now.ToString();
            container.SetMetadata();
        }

        static void GetMetaData(CloudBlobContainer container)
        {
            container.FetchAttributes();
            foreach (var item in container.Metadata)
            {
                Console.WriteLine("{0}: {1}",item.Key,item.Value);
            }
        }
    }
}
