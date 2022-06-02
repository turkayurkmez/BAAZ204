
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

public class Program
{
    static readonly string endPoint = "https://turka-cosmos.documents.azure.com:443/";
    static readonly string primaryKey = "TwT2LpE5hJmvDrViEnpsEijKd5doXnCTWWtp8JB0bEvGcXF8f0DMv6RvL1jAbvp10jzt5Kiem7Qfne6EuvecVA==";
    public async static Task Main(string[] args)
    {
        using (CosmosClient client = new CosmosClient(endPoint, primaryKey))
        {
            DatabaseResponse databaseResponse = await client.CreateDatabaseIfNotExistsAsync("Products");
            Database database = databaseResponse.Database;
            Console.WriteLine($"Database bulundu ya da oluşturuldu: {database.Id}");

            var containerProperties = new ContainerProperties("Clothing", "/PartitionKey");

            ContainerResponse containerResponse = await database.CreateContainerIfNotExistsAsync(containerProperties,10000);
            var container = containerResponse.Container;
            Console.WriteLine($"Container bulundu ya da oluşturuldu: {container.Id}");

            var product = new Product
            {
                Id = "8",
                Name = "Çanta",
                Price = 100,
                Color="Blue"
                
            };

            ItemResponse<Product> productResponse = await container.CreateItemAsync<Product>(product);
            


            var sqlQuery = "SELECT * FROM c";// WHERE c.Name = @name";


            QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);//.WithParameter("@name", "Tişört");
            var result = await container.GetItemQueryIterator<Product>(queryDefinition).ReadNextAsync();
            foreach (var item in result)
            {
                Console.WriteLine($"Ürün adı: {item.Name}\tFiyatı: {item.Price}");
            }
            

          

        }
      
    }
}

public class Product
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("price")]
    public double Price { get; set; }
    [JsonProperty("size")]
    public string Size { get; set; }
    [JsonProperty("color")]
    public string Color { get; set; }
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
    

}
