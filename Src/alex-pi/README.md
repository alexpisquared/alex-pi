# Chrono - 

https://dev.azure.com/AlexPi/_git/alex-pi?_a=history

## 2018-05
### Creating PWA with Angular 6 using Angular Material
www.youtube.com/watch?v=0UKJbtdPx4I

cd .\ng0poc\
npm install -g @angular/cli
ng --version
ng new my-pwa  --style=scss  --routing
cd .\my-pwa\
code .
ng serve 
ng add      @angular/material
ng generate @angular/material:material-nav    --name="app-nav"
ng generate @angular/material:material-table  --name="app-table"
ng add      @angular/pwa

npm install lite-server --save-dev
add to package.json: "start:prod": "ng build --prod && lite-server --baseDir dist/pwa-on20190904",
npm run start:prod

ng build --base-href "/xApi/" --prod
robocopy D:\gh\x\ng0poc\my-pwa\dist\my-pwa\ \\nes-corp\wwwroot\xApi\ -mir


## 2020-12 - Angular 10 Upgrades are Still Bad News!!!!! - John Peters - 2020-10-27 - https://dev.to/jwp/angular-upgrades-are-bad-news-21l2
## 2020-12-14  Upgrading NG from 8 to 11 - globally and locally - succeeded!!

## 2021-01
Guestbook - added visuals; need talk to SQL DB.
Prettier - Removed to avoid:
  1. auto-double-quoting
  2. 80 max length

## 2022-09-07
Updating to ng14 following this:  https://update.angular.io/?l=3&v=13.0-14.0

BTW, the build on Azure: https://dev.azure.com/AlexPi/alex-pi/_build
BTW, the repo  on Azure: https://dev.azure.com/AlexPi/alex-pi/_git/alex-pi

## 2022-09-08
Less at C:\Users\alexp\source\repos\alex-pi\AlexPiApi\ReadMe.md

## 2022-09-10   Created parallel alex0-pi:
⚡ alexp@RAZER1  ~\source\repos\alex-pi-22                                                                                                                                    [14:25]
❯ ng new alex-pi
? Would you like to add Angular routing? Yes
? Which stylesheet format would you like to use? SCSS   [ https://sass-lang.com/documentation/syntax#scss                ]


^^ Angular part ==============================================


#2019-09:
  FOLLOWING THESE: Getting Started with EF Core on ASP.NET Core with an Existing Database  https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db

"Server=.\sqlexpress;Database=OneBase;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model
"data source=sqs.database.windows.net;initial catalog=OneBase;persist security info=True;user id=APiSqlLogin;password=\"<▄▀▄▀▄▀use new for 2021 password▄▀▄▀▄▀>\";MultipleActiveResultSets=True;App=EntityFramework"

  Skip this, use next 7 lines below: SCAFFOLDING OFF azure db : https://cmatskas.com/scaffolding-dbcontext-and-models-with-entityframework-core-2-0-and-the-cli/  dotnet ef dbcontext scaffold "▄▀▄▀▄▀Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=False;User ID=APiSqlLogin;Password=<▄▀▄▀▄▀use new for 2021 password▄▀▄▀▄▀>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=12;" Microsoft.EntityFrameworkCore.SqlServer -o Model -f -c OneBaseContext

Model gen-n:
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Scaffold-DbContext "▄▀▄▀▄▀Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=True;user id=APiSqlLogin;Password='<▄▀▄▀▄▀use new for 2021 password▄▀▄▀▄▀>';" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model
Scaffold-DbContext "▄▀▄▀▄▀Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=True;user id=APiSqlLogin;Password='<▄▀▄▀▄▀use new for 2021 password▄▀▄▀▄▀>';" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -force
// ^^up - worked !!!
// down - not tried
Scaffold-DbContext "▄▀▄▀▄▀Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=False;user id=APiSqlLogin;Password='<▄▀▄▀▄▀use new for 2021 password▄▀▄▀▄▀>';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=12;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model

// seems OK without this one Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design 

  
SWAGGER part:   
{
- for Core 2:    Install-Package Swashbuckle.AspNetCore
- for Core 3:    Install-Package Swashbuckle.AspNetCore ~~Version="5.0.0-rc2"   !!!rc3 does not publish!!!

  paste code snippets from: https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-aspnet-core-ef-step-04?view=vs-2019#adding-swagger

  does not work with .NET Core 3.0 => downgraded to 2.2 !!!
}

Deploy to Azure:      https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-aspnet-core-ef-step-05?view=vs-2019
==> was it this one:  https://docs.microsoft.com/en-us/visualstudio/deployment/quickstart-deploy-aspnet-web-app?view=vs-2019&tabs=azure

#2020-11-10
  - Unparking and changing status to Available.
  - Iress logo added to my-clients.
  - npm update

#2020-12
  - Upgraded to
    - .Net 5.0
    - NG 11 (echo 2020-12-14  Upgrading NG from 8 to 11 - globally and locally - succeeded!!)
    - Safe storage of app secrets in development in ASP.NET Core  https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#register-the-user-secrets-configuration-source
      + done on SecretManagerIntro branch
      + apparently it is almost the same with my solution for AlexPi.scr and TypeCatch
    - Azure Key Vault Configuration Provider in ASP.NET Core      https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-5.0
      + pending Azure pricing clarification
        * secret creation   https://www.youtube.com/watch?v=PgujSug1ZbI
        * assign to an app  https://www.youtube.com/watch?v=6fjBGGrbayU
        * code to use Azure https://www.youtube.com/watch?v=6l_kpygO0Ic&feature=emb_logo

#2020-12-16  Azure Key Vault - Used https://www.youtube.com/watch?v=k2VYcYS3EIA !!! from 2019-12-31  <== worked!!! + https://github.com/Azure-Samples/key-vault-dotnet-core-quickstart

  https://alex-pi-api.azurewebsites.net
  48ebcf3d-868a-4fbe-adeb-5f91be20249f  alex-pi-api identity

#2021-01-12  Guestbook

#2021-12-13  Guestbook merged/published/deleted;  late2021updates created
  mode operandy: use GitHub for Desktop to perform local commits + VS to push/pull !!!

#2021-12-14    
  Broken publish was remedied by https://stackoverflow.com/questions/58107017/azure-app-service-deploy-failed-to-get-resource-id-for-resource-type-microsoft
  //todo: figure out dev/prod on ng13

#2021-12-15
  Is AlexPiApi a part of the CD/CI?

#2022-09-08
  More at C:\Users\alexp\Source\Repos\alex-pi\alex-pi\README.md

#2022-09-09
  Mar 19, 2022  CI/CD of Angular 13 app using Azure DevOps Pipelines      https://www.youtube.com/watch?v=iX1vHFghCtQ      Using YAML - not like mine.
  Jun 01, 2022  How to deploy Angular app to Azure app service using CI/CD pipeline from GIT repo | Angular | LSC     https://www.youtube.com/watch?v=24w65VnlrOU

  https://www.google.com/search?q=deploy+Angular+app+to+Azure+app+service+using+CI/CD+pipeline&tbm=vid&sxsrf=ALiCzsZ2Q03zrwQ0RbDOkj_vpslRU8NOsA:1662752222950&source=lnt&tbs=qdr:y&sa=X&ved=2ahUKEwjBmuTcuoj6AhWOrIkEHXJ_BcsQpwV6BAgKECA&biw=3724&bih=978&dpr=1.38
  
  
  ***********************************************************************************************************************************************
  https://www.youtube.com/watch?v=FW2-_ce_eNc   2021-10   Deploying Angular to Azure   |  Followed! Uses VSCode's Azure App Service extension.  *
  the key:                                                                                                                                      *
  - Compile build with     ng build --configuration=production                                                                                  *
  - Open VSCode at folder  C:\Users\alexp\source\repos\alex-pi\alex-pi\dist\alex-pi                                                             *                                               
  - Do the video steps                                                                                                                          *
  ***********************************************************************************************************************************************

  https://www.youtube.com/watch?v=NFqrWsUPCAM   2019-03   Angular CI/CD with Azure Devops to Azure App Service  |  Very similar to my setup. Try it!

  https://www.youtube.com/watch?v=9PTFz_hTEpU   2022-..   Angular App Azure Deployment |  Azure Static Web Hosting
  https://www.youtube.com/watch?v=vrQ4tPaj4MY   2022-..   DevOps | Azure | App Service | .NET 6 | Release Pipeline for .NET Core 6 Web API

  Publish and download pipeline Artifacts .. using YAML   https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/pipeline-artifacts?view=azure-devops&tabs=yaml

  Quickstart: Deploy an ASP.NET web app - LOOK: THERE IS A FREE TIER - https://docs.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net60&pivots=development-environment-vs#publish-your-web-app

#2022-09-10   From old days:
  https://stackoverflow.com/questions/65858151/changing-package-from-microsoft-extensions-configuration-azurekeyvault-to-azu
  https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-6.0
  https://medium.com/dotnet-hub/use-azure-key-vault-with-net-or-asp-net-core-applications-read-azure-key-vault-secret-in-dotnet-fca293e9fbb3

  Quickstarts original: https://docs.microsoft.com/en-us/azure/app-service/

# 2022-09-12
  Looks like the org  https://docs.microsoft.com/en-us/azure/devops/pipelines/release/?view=azure-devops
  ..followed up, created new Rls Pipeline "~Org rls pipeline" with approvals..
  ..only to notice that the old one "Alex-Pi--CD" works but for the root folder only!!!!!!??????

  !!!Apparently, all has been well: building, publishing, all!!! It must've been cached version got stuck. Used title in the index.html to figure it out: looks like it does not get cached.

# 2024-01-11  Started getting this error:

2024-01-11T00:44:12.3149903Z ##[error]Could not fetch access token for Azure. Verify if the Service Principal used is valid and not expired. For more information refer https://aka.ms/azureappservicedeploytsg
2024-01-11T00:44:12.3159748Z (node:4684) UnhandledPromiseRejectionWarning: TypeError: Cannot read property 'getApplicationURL' of undefined
2024-01-11T00:44:12.3162271Z     at WindowsWebAppRunFromZipProvider.<anonymous> (D:\a\_tasks\AzureRmWebAppDeployment_497d490f-eea7-4f2b-ab94-48d9c1acdcb1\4.232.2\deploymentProvider\WindowsWebAppRunFromZipProvider.js:68:77)

2024-01-11T00:44:12.3165110Z ##[error]Error: Failed to get resource ID for resource type 'Microsoft.Web/Sites' and resource name 'Alex-Pi'. 
  Error: Could not fetch access token for Azure. 
  Status code: invalid_client, status message: 7000222 - [2024-01-11 00:44:12Z]: 
  AADSTS7000222: The provided client secret keys for app '***' are expired. 
      Visit the Azure portal to create new keys for your app: https://aka.ms/NewClientSecret, or 
      consider using certificate credentials for added security: https://aka.ms/certCreds. 
  Trace ID: d6a190bc-5d8c-4f60-9f76-7e09f307c403 Correlation ID: da4a2171-dd77-4f5c-a188-c14ef0a5e529 Timestamp: 2024-01-11 00:44:12Z - Correlation ID: da4a2171-dd77-4f5c-a188-c14ef0a5e529 - 
  Trace ID: d6a190bc-5d8c-4f60-9f76-7e09f307c403

2024-01-11T00:44:12.3166724Z     at Generator.next (<anonymous>)
2024-01-11T00:44:12.3170616Z     at fulfilled (D:\a\_tasks\AzureRmWebAppDeployment_497d490f-eea7-4f2b-ab94-48d9c1acdcb1\4.232.2\deploymentProvider\WindowsWebAppRunFromZipProvider.js:5:58)
2024-01-11T00:44:12.3171418Z Failed to add release annotation. TypeError: Cannot read property 'getApplicationSettings' of undefined
2024-01-11T00:44:12.3172029Z     at process._tickCallback (internal/process/next_tick.js:68:7)
2024-01-11T00:44:12.3172772Z (node:4684) UnhandledPromiseRejectionWarning: Unhandled promise rejection. This error originated either by throwing inside of an async function without a catch block, or by rejecting a promise which was not handled with .catch(). (rejection id: 1)
2024-01-11T00:44:12.3173942Z (node:4684) [DEP0018] DeprecationWarning: Unhandled promise rejections are deprecated. In the future, promise rejections that are not handled will terminate the Node.js process with a non-zero exit code.
2024-01-11T00:44:12.3245118Z ##[section]Finishing: Deploy Azure App Service

1. I added two new secrets:
  AlexPi-CICD2021-55444df9-fdd4-43d1-a454-16a90de75646 | Certificates & secrets
    Alex-Pi--CD fix attempt  1/9/2026  ol.8Q~Jxl06-V5eZaxJKrxQK~zyCowOItP6_NbY2  76fee11c-b163-47cc-af54-21c71fd4c89b
      ^^^^^^^^^^!!!!!!!!!!REMOVED!!!!!!!!!!^^^^^^^^^^^

  https://aka.ms/NewClientSecret: In the Ms Entra admin center, in App registrations, select your application \ LocalDevelopmentEnv \ BuildErrFix2024 => .wb8Q~TgvijRAfrg5RgGgmjaXHEjoSwwVDjrCb..
    Description  Expires  Value  Secret ID
    BuildErrFix2024  1/10/2026  .wb8Q~TgvijRAfrg5RgGgmjaXHEjoSwwVDjrCb..  53b91c1f-20f3-426d-8d60-c817202649ae
      ^^^^^^^^^^!!!!!!!!!!REMOVED!!!!!!!!!!^^^^^^^^^^^
    DevSecret  12/31/2299  ~8H******************  612ef2b3-fd91-4e7b-a8eb-fb620137e0a0

2. Then tried to follow this: 
  Copilot from Edge:
    To update the client secret in your Azure DevOps pipeline for the “Azure App Service Deploy” stage, you’ll need to update the service connection used by that stage. Here’s a general process:
  
    Go to your Azure DevOps project.
    Navigate to Project settings at the bottom of the left-hand menu.
    Click on Service connections under the Pipelines section.
    Find the service connection used by your release stage. This is typically an Azure Resource Manager connection.
    Click on the Edit button for that service connection.
    ??  Update the Client secret field with the new client secret you generated in Azure AD.
      No such field; BUT I UPDATED AN EMPTY Resource Group combobox with 'uvw' ... THAT SEEMS TO HAVE SOLVED THE PROBLEM!?!?!?!?!?!?!?!?!?!?!? *********************************  
        Also, there is a Verify button. Clicking gives green check now ...not sure what was there before.

    ??  Click Verify and save.
    ??  This should update the client secret used by your pipeline. The next time you run the pipeline, it should use the new client secret for the “Azure App Service Deploy” stage.
  Not sure what happened, but it worked. I did not have luck with the last 3 ?? steps... It just worked. I guess it was a glitch in the matrix.

?? maybe try GitHub actions:  https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions?tabs=applevel

## 2024-07-13
//tu: Replacing the angular.json with the one from the working Ng17 version fixed the target path issue. The build is now successful.

branch-from-90290028-LastGoodNg18-PreMerge

## 2025-03-03  Failure to get new artifacts

