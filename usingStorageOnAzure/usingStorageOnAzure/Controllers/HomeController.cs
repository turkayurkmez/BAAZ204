using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using usingStorageOnAzure.Models;

namespace usingStorageOnAzure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public IActionResult UploadFile()
        {
          
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // TODO 1: Upload işlemi için blob storage'a dosya yükleme işlemi yapılacak.
            if (file == null || file.Length ==0)
            {
                ViewBag.Message = "E dosya seçmedin?";
                return View();                   
                
            }
            var blobServiceClient = CreateServiceClient();
            var containerName = "books";            

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            _logger.LogInformation("Container {containerName} çekildi", containerName);
            var blobClient = containerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            _logger.LogInformation($"{file.Name} dosyası upload edildi");


            return View();
        }

        public async Task<IActionResult> ListOfBlobsInContainer()
        {
            var blobServiceClient = CreateServiceClient();
            var containerName = "books";
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = containerClient.GetBlobsAsync();           
            return View(blobs);
           
        }

        public async Task<IActionResult> DownloadFile(string blobName)
        {
            var blobServiceClient = CreateServiceClient();
            var containerName = "books";
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var download = await blobClient.DownloadAsync();
            return File(download.Value.Content, "application/octet-stream", blobName);
        }

        private BlobServiceClient CreateServiceClient() {
            var blobConnection = configuration.GetSection("AzureStorage").GetValue<string>("ConnectionString");
            var blobServiceClient = new BlobServiceClient(blobConnection);
            return blobServiceClient;

        }
            
    }
}