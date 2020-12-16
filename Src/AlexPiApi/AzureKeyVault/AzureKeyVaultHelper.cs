using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Azure.Identity;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using System;

namespace AlexPiApi.AzureKeyVault
{

  public class TypeMappingDemo
  {
    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
    public string Prop3 { get; set; }
  }

  public class AzureKeyVaultHelper
  {
    public static void TestLocalSecrets(IConfiguration cfg)
    {
      var tmd = new TypeMappingDemo();
      cfg.Bind(nameof(TypeMappingDemo), tmd);
      Debug.WriteLine($"** Prop1:  '{tmd.Prop1}'.");

      Debug.WriteLine($"** Local - '{cfg["WebApiStuff:DbOneConStr"]}'  ");
      Debug.WriteLine($"** Azure - '{cfg["ConnectionStrings:OneBaseDbConStr"]}' - never worked yet ");
      Debug.WriteLine($"** Azure - {GetUsingSecretClient().Result}  ");
      Debug.WriteLine($"** Azure - {GetUsingKeyVaultClient().Result}  ");
    }

    public static async Task<string> GetUsingSecretClient() // https://youtu.be/TU82BTmeNeU?t=90
    {
      var sclient = new SecretClient(
        new Uri("https://demopockv.vault.azure.net"),
        new DefaultAzureCredential());

      return (await sclient.GetSecretAsync("OneBaseDbConStr")).Value.Value;
    }
    public static async Task<string> GetUsingKeyVaultClient() // https://youtu.be/TU82BTmeNeU?t=90
    {
      var azureCredOptions = new DefaultAzureCredentialOptions();
#if DEBUG
      azureCredOptions.SharedTokenCacheUsername = "kvvsuser@alexpigidaoutlook.onmicrosoft.com";
#endif

      var credential = new DefaultAzureCredential(azureCredOptions);
      var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
      {
        var token = await credential.GetTokenAsync(
          new Azure.Core.TokenRequestContext(
            new[] { "https://vault.azure.net/.default" }, null));
        return token.Token;
      });

      return (await keyVaultClient.GetSecretAsync("https://demopockv.vault.azure.net/secrets/~? my testsecrets/??? 24j234l23kj4l23j")).Value;
    }
    public static void lkj() // https://docs.microsoft.com/en-us/dotnet/api/overview/azure/service-to-service-authentication#using-the-library
    {
      // Use AzureServiceTokenProvider’s built-in callback for KeyVaultClient
      var azureServiceTokenProvider = new AzureServiceTokenProvider();
      var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

      //// Request an access token for SqlConnection
      //var sqlConnection = new SqlConnection(YourConnectionString)) 
      //  {
      //  sqlConnection.AccessToken = azureServiceTokenProvider.GetAccessTokenAsync("https://database.windows.net");
      //  sqlConnection.Open();
      //}
    }
  }
}