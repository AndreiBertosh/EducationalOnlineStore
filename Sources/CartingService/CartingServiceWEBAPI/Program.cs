using Asp.Versioning;
using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Interfaces;
using CartingServiceWEBAPI.Providers;
using CartingServiceWEBAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SwaggerVersioningTestWEBAPI.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
//using Microsoft.AspNetCore.Mvc.Versioning;

namespace CartingServiceWEBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddApiVersioning()
                .AddApiExplorer(options =>
                {
                    // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;

                });

            builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            builder.Services.AddApiVersioning(options =>
            {
                //indicating whether a default version is assumed when a client does
                // does not provide an API version.
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1.0);
            });

            // Add services to the container.
//           builder.Services.AddGrpc();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen(options =>
            {
                // Add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddSingleton<ISettingsProvider, SettingsProvider>();
            builder.Services.AddSingleton<ICartProvider, CartProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var descriptions = app.DescribeApiVersions();

                    // Build a swagger endpoint for each discovered API version
                    foreach (var description in descriptions)
                    {
                        string url = $"/swagger/{description.GroupName}/swagger.json";
                        string name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });
                app.UseSwagger(options => { options.RouteTemplate = "api-docs/{documentName}/docs.json"; });
            }

            // Configure the HTTP request pipeline.
            //app.MapGrpcService<GreeterService>();
            //app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapControllerRoute(name: "default", pattern: "{controller=CartingServiceControllerV1}/{action=index}/{id?}");
            //app.MapControllerRoute(name: "v2", pattern: "{controller=CartingServiceControllerV2}/{action=index}/{id?}");

            app.Run();
        }
    }
}