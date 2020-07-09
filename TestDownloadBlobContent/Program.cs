using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.IO;

namespace TestDownloadBlobContent
{
    class Program
    {
        public static async System.Threading.Tasks.Task Main()
        {
            try
            {
                string storageAccountConnectionString = "TO DO- CONNECTION STRING";
                string containerName = "test-downloadcontent";
                string blobName = "UsingXMLDoc1.xml"; //"UsingFileStream1.xml";

                //string contentUsingDownloadTextAsync = await DownloadContentAsync_DownloadTextAsync(storageAccountConnectionString, containerName, blobName);
                string contentUsingDownloadTOStreamAsync = await DownloadContentAsync_DownloadToStreamAsync(storageAccountConnectionString, containerName, blobName);
            }
            catch(Exception e)
            {

            }
        }

        private static CloudBlobContainer GetContainer(string storageAccountConnectionString, string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            return container;
        }

        private static async System.Threading.Tasks.Task<string> DownloadContentAsync_DownloadTextAsync(string storageAccountConnectionString, string containerName, string blobName)
        {
            CloudBlobContainer container = GetContainer(storageAccountConnectionString, containerName);
            ICloudBlob blob = await container.GetBlobReferenceFromServerAsync(blobName);

            // Download the blob content
            string xmlBlobContent =
                await (blob as CloudBlockBlob).DownloadTextAsync(
                    null,
                    new BlobRequestOptions { LocationMode = LocationMode.PrimaryThenSecondary },
                    new OperationContext());

            return xmlBlobContent;
        }

        private static async System.Threading.Tasks.Task<string> DownloadContentAsync_DownloadToStreamAsync(string storageAccountConnectionString, string containerName, string blobName)
        {
            CloudBlobContainer container = GetContainer(storageAccountConnectionString, containerName);
            ICloudBlob blob = await container.GetBlobReferenceFromServerAsync(blobName);

            // Download the blob content
            MemoryStream resultStream = new MemoryStream();
            await (blob as CloudBlockBlob).DownloadToStreamAsync(
                resultStream,
                null,
                new BlobRequestOptions { LocationMode = LocationMode.PrimaryThenSecondary },
                new OperationContext());
            string xmlBlobContent = System.Text.Encoding.UTF8.GetString(resultStream.ToArray());

            return xmlBlobContent;
        }
    }
}
