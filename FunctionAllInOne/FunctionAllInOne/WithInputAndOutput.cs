using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAllInOne
{
    public static class WithInputAndOutput
    {
        [FunctionName("WithInputAndOutput")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            
            [CosmosDB(databaseName:"func-io", collectionName:"Bookmarks", Id ="{Query.Id}", PartitionKey = "{Query.PartitionKey}", ConnectionStringSetting = "CosmosConnection")] Bookmark bookmark,
            [CosmosDB(databaseName: "func-io", collectionName: "Bookmarks", Id = "{Query.Id}", PartitionKey = "{Query.PartitionKey}", ConnectionStringSetting = "CosmosConnection")] out Bookmark newBookmark,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            newBookmark = null;

            if (bookmark == null)
            {
                log.LogInformation($"Yok böyle bir bookmark");
                //return new NotFoundResult();
                newBookmark = new Bookmark
                {
                    Id = req.Query["Id"],
                    Url = req.Query["Url"]
                    
                };

                return new OkObjectResult(new { message = $"Yeni bookmark eklendi. Url adresi: {newBookmark.Url}", bookmark = newBookmark });
            }
            else
            {
                log.LogInformation($"{bookmark.Id} id'li url: {bookmark.Url}");
                return new OkObjectResult(bookmark);
            }

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);
        }
    }

    public class Bookmark
    {
        [JsonProperty("id")]
        public string Id { get; set; }      

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
