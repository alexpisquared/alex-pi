using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace AlexPiApi;

public class Program
{
  public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

  public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((context, config) =>
          {
            var keyVaultEndpoint = new Uri("https://demopockv.vault.azure.net/");
            _ = config.AddAzureKeyVault(
                    keyVaultEndpoint,
                    new DefaultAzureCredential()
                    //,                    new AzureKeyVaultConfigurationOptions()
                    );
          })
          .ConfigureWebHostDefaults(webBuilder =>
          {
            _ = webBuilder.UseStartup<Startup>();
          });
}

/*
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace AlexPiApi;

public class Program
{
  public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

  public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((ctx, builder) => builder.AddAzureKeyVault("https://demopockv.vault.azure.net/", new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback)), new DefaultKeyVaultSecretManager())) //tu: !!! MVP for Azure Key Vault utilization !!!   https://github.com/Azure-Samples/key-vault-dotnet-core-quickstart
          .ConfigureWebHostDefaults(webBuilder =>
          {
            _ = webBuilder.UseStartup<Startup>();
          });
}
*/