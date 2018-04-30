using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StorageAccess.Models;

namespace StorageAccess.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudBlobClient blobClient = storage.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            var blobs = new List<BlobImages>();
            foreach (var blob in container.ListBlobs())
            {
                if (blob.GetType()==typeof(CloudBlockBlob))
                {
                    var sas = container.GetSharedAccessSignature(null, "MySAP"); //
                    blobs.Add(new BlobImages{blobUri = blob.Uri.ToString() + sas});
                }
            }

            return View(blobs);
        }

        //static string GetSASToken(CloudStorageAccount storageAccount)
        //{
        //    SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
        //    {
        //        Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write,
        //        Services = SharedAccessAccountServices.Blob,
        //        ResourceTypes = SharedAccessAccountResourceTypes.Object,
        //        SharedAccessExpiryTime = DateTime.Now.AddMinutes(30),
        //        Protocols = SharedAccessProtocol.HttpsOnly
        //    };

        //    return storageAccount.GetSharedAccessSignature(policy);
        //}
    }
}