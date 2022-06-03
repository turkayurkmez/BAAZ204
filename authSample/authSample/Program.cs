// See https://aka.ms/new-console-template for more information
using Microsoft.Identity.Client;

string clientID = "fe70dc4a-b3f3-4ca2-9234-e57b0d6dbf09";
string directoryId = "714700c6-10e2-4ca4-b315-06d7fe91ee94";

var app =PublicClientApplicationBuilder.Create(clientID)
    .WithAuthority(AzureCloudInstance.AzurePublic, directoryId)
    .WithRedirectUri("http://localhost")
    .Build();

var scopes = new string[] { "user.read" };
AuthenticationResult result = await app.AcquireTokenInteractive(scopes)
    .ExecuteAsync();

Console.WriteLine(result.AccessToken);

