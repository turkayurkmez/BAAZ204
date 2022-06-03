using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;

namespace KeyVaultSample
{
    public static class Function1
    {
        [FunctionName("AnotherFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            var connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            try
            {
                log.LogInformation($"Checking container.....");
                log.LogInformation("Okunan data:" + connectionString);
                BlobClient blobClient = new BlobClient(connectionString, "images", "seminer.jpg");
                log.LogInformation($"Blob client instance generated.....");
                log.LogInformation($"container data: {blobClient.AccountName} container name: {blobClient.BlobContainerName}, Uri: {blobClient.Uri}");
                var response = await blobClient.DownloadAsync();
                log.LogInformation("Download completed");
                return new FileStreamResult(response?.Value?.Content, response?.Value?.ContentType);
            }
            catch (Exception ex)
            {

                log.LogInformation($"ERROR !!!! {ex.Message}: {ex.InnerException?.Message}");
                return new OkObjectResult(new { message = $"ERROR !!!! {ex.Message}: {ex.InnerException?.Message}" });
            }
          
            
           
        }
    }
}
