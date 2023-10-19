using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
//Microsoft.Azure.Storage

namespace BlobTriggerJayanth
{
    public class Function1
    {

        public static void Run(
         [BlobTrigger("democontainer/sample.txt", Connection = "StorageAccountConnectionString")] Stream myBlob,
         [Blob("outputcontainer/sample.txt", FileAccess.Write, Connection = "StorageAccountConnectionString")] Stream outputBlob,
         ILogger log)
        {
            log.LogInformation($"C# Blob trigger function processed blob\n Name:{myBlob}");

            // Read the content of the source blob (sample.txt)
            using (StreamReader reader = new StreamReader(myBlob))
            {
                string content = reader.ReadToEnd();
                // Write the content to the target blob with the same name
                using (StreamWriter writer = new StreamWriter(outputBlob))
                {
                    writer.Write(content);
                }
            }
        }


    }
}
