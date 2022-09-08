/*
https://stackoverflow.com/questions/65858151/changing-package-from-microsoft-extensions-configuration-azurekeyvault-to-azu
https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-6.0
https://medium.com/dotnet-hub/use-azure-key-vault-with-net-or-asp-net-core-applications-read-azure-key-vault-secret-in-dotnet-fca293e9fbb3

*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Azure.Extensions.AspNetCore.Configuration.Secrets; // see comments above ^^
using Microsoft.Extensions.Hosting;
using Azure.Identity;

namespace AlexPiApi
{
  public class Program
  {
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, builder) => builder.AddAzureKeyVault("https://demopockv.vault.azure.net/", new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback)), new DefaultKeyVaultSecretManager())) //tu: !!! MVP for Azure Key Vault utilization !!!   https://github.com/Azure-Samples/key-vault-dotnet-core-quickstart
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
