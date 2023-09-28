using AlexPiApi.Services;
using Db.OneBase.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AlexPiApi;

public class Startup
{
  public Startup(IConfiguration configuration) => Configuration = configuration;

  public IConfiguration Configuration { get; }

  readonly string _allowSpecificOriginsPolicyName = "_someName 'Enable Cross-Origin Requests (CORS) in ASP.NET Core'"; // see more at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2

  public void ConfigureServices(IServiceCollection services) // This method gets called by the runtime. Use this method to add services to the container.
  {
    _ = services.AddCors(options => // see more at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2
    {
      options.AddPolicy(
        _allowSpecificOriginsPolicyName,
        builder =>
        {
          _ = builder.
            WithOrigins
            (
              "http://localHost:4200",
              "https://alex-pi.azurewebsites.net"
            )
            .AllowAnyHeader()
            .AllowAnyMethod(); // needed for Delete calls.              
        });
    });

    _ = services.AddDbContext<OneBaseContext>(options => options.UseSqlServer(Configuration["OneBaseDbConStr"])); //tu: !!! MVP for Azure Key Vault utilization !!!     //tu: inject DbContext in .NET Core.

    _ = services.AddSingleton<ITextDbContext>(new TextDbContext(Configuration["ChtBlobStorageConnectionString"]));

    _ = services.AddControllers();

    _ = services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);      // services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter(true)); });

    ///ap
    /// https://medium.com/@balramchavan/generate-angular-ionic-client-code-from-openapi-swagger-rest-api-specifications-128a6899681a
    _ = services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1", Description = "Demo API for swagger code generation" }); });
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
  {
    if (env.IsDevelopment())
    {
      _ = app.UseDeveloperExceptionPage();
    }

    _ = app.UseCors(_allowSpecificOriginsPolicyName);  // see more at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2

    _ = app.UseHttpsRedirection();
    _ = app.UseRouting();
    _ = app.UseAuthorization();
    _ = app.UseEndpoints(endpoints =>
    {
      _ = endpoints.MapControllers();
    });

    ///ap
    /// https://medium.com/@balramchavan/generate-angular-ionic-client-code-from-openapi-swagger-rest-api-specifications-128a6899681a
    /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
    _ = app.UseSwagger();       // Enable middleware to serve generated Swagger as a JSON endpoint.
    _ = app.UseSwaggerUI(c =>   // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    {
      c.SwaggerEndpoint("../swagger/v1/swagger.json", "My API V1");
    });
  }
}