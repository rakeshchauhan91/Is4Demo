// See https://aka.ms/new-console-template for more information
using static IdentityModel.OidcConstants;
using System.Net;
using IdentityModel.Client;
using System.Text.Json;
 




var token = await GetToken();
Console.WriteLine($"token {token}");
await CallAPI(token);
Console.ReadKey();



static async Task<string> GetToken()
{
    // request token
    var client = new HttpClient();
    var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5101");
    if (disco.IsError)
        Console.WriteLine(disco.Error);

    var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
        Address = disco.TokenEndpoint,

        ClientId = "m2mClient",
        ClientSecret = "secret",
        Scope = "api1"
    });

    if (tokenResponse.IsError)
    {
        Console.WriteLine(tokenResponse.Error);
        return "";
    }
    return tokenResponse.AccessToken;
}



static async Task CallAPI(string token)
{

    // call api
    var apiClient = new HttpClient();
    apiClient.SetBearerToken(token);

    var response = await apiClient.GetAsync("https://localhost:7003/identity");
    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine(response.StatusCode);
    }
    else
    {
        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
        Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
    }

}