using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AlexPiApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            })
#if !true // using local store
            .ConfigureAppConfiguration((hostContext, builder) =>  /// ap  Safe storage of app secrets in development in ASP.NET Core  https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#register-the-user-secrets-configuration-source
            {                                                     /// ap  Safe storage of app secrets in development in ASP.NET Core  
              // Add other providers for JSON, etc.               /// ap  Safe storage of app secrets in development in ASP.NET Core  

              if (hostContext.HostingEnvironment.IsDevelopment()) /// ap  Safe storage of app secrets in development in ASP.NET Core  
              {                                                   /// ap  Safe storage of app secrets in development in ASP.NET Core  
                builder.AddUserSecrets<Program>();                /// ap  Safe storage of app secrets in development in ASP.NET Core  
              }
            });
#else   // using Azure Key Vault
            .ConfigureAppConfiguration((context, config) =>  
            {
              var bldcfg = config.Build();
              //todo: var vaultName = bldcfg["VaultName"];
              var vaultName = "https://demopockv.vault.azure.net/"; //todo: get from above/appsettings.json

              var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
              {
                var credential = new DefaultAzureCredential(false);
                var token = credential.GetToken(
                  new Azure.Core.TokenRequestContext(
                    new[] { "https://vault.azure.net/.default" }));
                return token.Token;
              });
              config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
            });
    /*
Azure.Identity.AuthenticationFailedException
  HResult = 0x80131500
  Message=SharedTokenCacheCredential authentication failed: 
    A configuration issue is preventing authentication - check the error message from the server for details.
    You can modify the configuration in the application registration portal.
    See https://aka.ms/msal-net-invalid-client for details.  
    Original exception: AADSTS70002: The client does not exist or is not enabled for consumers. 
    If you are the application developer, configure a new application through the App Registrations in the Azure Portal at https://go.microsoft.com/fwlink/?linkid=2083908.
Trace ID: e880dd0a-ebda-47b3-87b1-3fdea8596200
Correlation ID: 7ae1e43c-e7da-4703-b8e3-488ca8adf754
Timestamp: 2020-12-15 17:21:44Z
  Source = Azure.Identity
  StackTrace:
   at Azure.Identity.CredentialDiagnosticScope.FailWrapAndThrow(Exception ex)
   at Azure.Identity.SharedTokenCacheCredential.<GetTokenImplAsync>d__18.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.ValidateEnd(Task task)
   at System.Threading.Tasks.ValueTask`1.get_Result()
   at System.Runtime.CompilerServices.ValueTaskAwaiter`1.GetResult()
   at Azure.Core.Pipeline.TaskExtensions.EnsureCompleted[T](ValueTask`1 task)
   at Azure.Identity.SharedTokenCacheCredential.GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
   at Azure.Identity.DefaultAzureCredential.<GetTokenFromSourcesAsync>d__14.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.ValidateEnd(Task task)
   at System.Threading.Tasks.ValueTask`1.get_Result()
   at System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable`1.ConfiguredValueTaskAwaiter.GetResult()
   at Azure.Identity.DefaultAzureCredential.<GetTokenImplAsync>d__12.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Azure.Identity.CredentialDiagnosticScope.FailWrapAndThrow(Exception ex)
   at Azure.Identity.DefaultAzureCredential.<GetTokenImplAsync>d__12.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.ValidateEnd(Task task)
   at System.Threading.Tasks.ValueTask`1.get_Result()
   at System.Runtime.CompilerServices.ValueTaskAwaiter`1.GetResult()
   at Azure.Core.Pipeline.TaskExtensions.EnsureCompleted[T](ValueTask`1 task)
   at Azure.Identity.DefaultAzureCredential.GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
   at AlexPiApi.Program.<>c.<<CreateHostBuilder>b__1_2>d.MoveNext() in C:\Users\alexp\source\repos\alex-pi\AlexPiApi\Program.cs:line 48
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1.ConfiguredTaskAwaiter.GetResult()
   at Microsoft.Azure.KeyVault.KeyVaultCredential.<PostAuthenticate>d__9.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1.ConfiguredTaskAwaiter.GetResult()
   at Microsoft.Azure.KeyVault.KeyVaultCredential.<ProcessHttpRequestAsync>d__10.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.Azure.KeyVault.KeyVaultClient.<GetSecretsWithHttpMessagesAsync>d__66.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1.ConfiguredTaskAwaiter.GetResult()
   at Microsoft.Azure.KeyVault.KeyVaultClientExtensions.<GetSecretsAsync>d__49.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.Extensions.Configuration.AzureKeyVault.AzureKeyVaultConfigurationProvider.<LoadAsync>d__11.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter.GetResult()
   at Microsoft.Extensions.Configuration.AzureKeyVault.AzureKeyVaultConfigurationProvider.Load()
   at Microsoft.Extensions.Configuration.ConfigurationRoot..ctor(IList`1 providers)
   at Microsoft.Extensions.Configuration.ConfigurationBuilder.Build()
   at Microsoft.Extensions.Hosting.HostBuilder.BuildAppConfiguration()
   at Microsoft.Extensions.Hosting.HostBuilder.Build()
   at AlexPiApi.Program.Main(String[] args) in C:\Users\alexp\source\repos\alex-pi\AlexPiApi\Program.cs:line 19

  This exception was originally thrown at this call stack:
    Microsoft.Identity.Client.OAuth2.OAuth2Client.ThrowServerException(Microsoft.Identity.Client.Http.HttpResponse, Microsoft.Identity.Client.Internal.RequestContext)
    Microsoft.Identity.Client.OAuth2.OAuth2Client.CreateResponse<T>(Microsoft.Identity.Client.Http.HttpResponse, Microsoft.Identity.Client.Internal.RequestContext)
    Microsoft.Identity.Client.OAuth2.OAuth2Client.ExecuteRequestAsync<T>(System.Uri, System.Net.Http.HttpMethod, Microsoft.Identity.Client.Internal.RequestContext, bool, bool)
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(System.Threading.Tasks.Task)
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(System.Threading.Tasks.Task)
    System.Runtime.CompilerServices.ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter.GetResult()
    Microsoft.Identity.Client.OAuth2.OAuth2Client.GetTokenAsync(System.Uri, Microsoft.Identity.Client.Internal.RequestContext, bool)
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(System.Threading.Tasks.Task)
    ...
    [Call Stack Truncated]

    Inner Exception 1:
MsalServiceException: A configuration issue is preventing authentication - check the error message from the server for details.
    You can modify the configuration in the application registration portal.See https://aka.ms/msal-net-invalid-client for details.  
    Original exception: AADSTS70002: The client does not exist or is not enabled for consumers. 
    If you are the application developer, configure a new application through the App Registrations in the Azure Portal at https://go.microsoft.com/fwlink/?linkid=2083908.

Trace ID: e880dd0a-ebda-47b3-87b1-3fdea8596200
Correlation ID: 7ae1e43c-e7da-4703-b8e3-488ca8adf754
Timestamp: 2020-12-15 17:21:44Z
*/
#endif
  }
}
