using Db.OneBase.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AlexPiApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    readonly string _allowSpecificOriginsPlicyName = "_someName 'Enable Cross-Origin Requests (CORS) in ASP.NET Core'"; // see more at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2

    public void ConfigureServices(IServiceCollection services) // This method gets called by the runtime. Use this method to add services to the container.
    {
      services.AddCors(options => // see more at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2
      {
        options.AddPolicy(
          _allowSpecificOriginsPlicyName,
          builder =>
          {
            builder.
              WithOrigins
              (
                "http://localHost:4200",
                "https://alex-pi.azurewebsites.net"
              )
              .AllowAnyHeader() // 
              .AllowAnyMethod() // needed for Delete calls.
              ;
          });
      });

      services.AddDbContext<OneBaseContext>( //tu: inject DbContext in .NET Core.
        options => options.UseSqlServer(Configuration["OneBaseDbConStr"])
      );


      services.AddControllers();

      services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);      // services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter(true)); });

      ///ap
      /// https://medium.com/@balramchavan/generate-angular-ionic-client-code-from-openapi-swagger-rest-api-specifications-128a6899681a
      services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1", Description = "Demo API for swagger code generation" }); });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(_allowSpecificOriginsPlicyName);  // see more at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      ///ap
      /// https://medium.com/@balramchavan/generate-angular-ionic-client-code-from-openapi-swagger-rest-api-specifications-128a6899681a
      /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
      app.UseSwagger();       // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwaggerUI(c =>   // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
      {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
      });

      System.Diagnostics.Debug.WriteLine($"** {Configuration["TestSecret1"]}"); //tu: !!! MVP for Azure Key Vault utilization !!!
      System.Diagnostics.Debug.WriteLine($"** {Configuration["OneBaseDbConStr"]}");
      System.Diagnostics.Debug.WriteLine($"** {Configuration["WhereAmI"]}");
    }
  }
}
//2020-11-10 - Test to ignore++