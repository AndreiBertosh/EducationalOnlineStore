
using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;
using CartingServiceWEBAPI.Interfaces;
using CartingServiceWEBAPI.Providers;

namespace CartingServiceWEBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<ISettingsProvider, SettingsProvider>();
            builder.Services.AddSingleton<ICartProvider, CartProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapControllerRoute(name: "default", pattern: "{controller=CartingServiceControllerV1}/{action=index}/{id?}");
            app.MapControllerRoute(name: "v2", pattern: "{controller=CartingServiceControllerV2}/{action=index}/{id?}");

            app.Run();
        }
    }
}