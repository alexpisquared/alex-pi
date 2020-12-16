#Chrono
#2019-09:
  FOLLOWING THESE: Getting Started with EF Core on ASP.NET Core with an Existing Database  https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db

"Server=.\sqlexpress;Database=OneBase;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model
"data source=sqs.database.windows.net;initial catalog=OneBase;persist security info=True;user id=APiSqlLogin;password=\"j4g4nFF83djrt77\";MultipleActiveResultSets=True;App=EntityFramework"

  Skip this, use next 7 lines below: SCAFFOLDING OFF azure db : https://cmatskas.com/scaffolding-dbcontext-and-models-with-entityframework-core-2-0-and-the-cli/  dotnet ef dbcontext scaffold "Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=False;User ID=APiSqlLogin;Password=j4g4nFF83djrt77;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=12;" Microsoft.EntityFrameworkCore.SqlServer -o Model -f -c OneBaseContext

Model gen-n:
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Scaffold-DbContext "Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=True;user id=APiSqlLogin;Password='j4g4nFF83djrt77';" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model
Scaffold-DbContext "Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=True;user id=APiSqlLogin;Password='j4g4nFF83djrt77';" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -force
// ^^up - worked !!!
// down - not tried
Scaffold-DbContext "Server=tcp:sqs.database.windows.net,1433;Initial Catalog=OneBase;Persist Security Info=False;user id=APiSqlLogin;Password='j4g4nFF83djrt77';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=12;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model

// seems OK without this one Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design 

  
SWAGGER part:   
{
- for Core 2:    Install-Package Swashbuckle.AspNetCore
- for Core 3:    Install-Package Swashbuckle.AspNetCore ~~Version="5.0.0-rc2"   !!!rc3 does not publish!!!

  paste code snippets from: https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-aspnet-core-ef-step-04?view=vs-2019#adding-swagger

  does not work with .NET Core 3.0 => downgraded to 2.2 !!!
}

  Next Step: Deploy to Azure: https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-aspnet-core-ef-step-05?view=vs-2019

#2020-11-10
  - Unparking and changing status to Available.
  - Iress logo added to my-clients.
  - npm update

#2020-12
  - Upgrade to
    - .Net 5.0
    - NG 11 (echo 2020-12-14  Upgrading NG from 8 to 11 - globally and locally - succeeded!!)
    - Safe storage of app secrets in development in ASP.NET Core  https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#register-the-user-secrets-configuration-source
      + done on SecretManagerIntro branch
      + apparently it is almost the same with my solutiuon for AlexPi.scr and TypeCatch
    - Azure Key Vault Configuration Provider in ASP.NET Core      https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-5.0
      + pending Azure pricing clarification
        * secret creation   https://www.youtube.com/watch?v=PgujSug1ZbI
        * assign to an app  https://www.youtube.com/watch?v=6fjBGGrbayU
        * code to use Azure https://www.youtube.com/watch?v=6l_kpygO0Ic&feature=emb_logo

#2020-12-15 //todo: https://www.youtube.com/watch?v=k2VYcYS3EIA from 2019-12-31

