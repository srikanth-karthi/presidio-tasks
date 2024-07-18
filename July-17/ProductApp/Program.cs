
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using ProductApp.Context;
using ProductApp.Interfaces;
using ProductApp.Models;
using ProductApp.Repository;
using ProductApp.Services;

namespace ProductApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Context
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var keyVaultUri = builder.Configuration["KeyVault:VaultUri"];

            if (!string.IsNullOrEmpty(keyVaultUri))
            {
                var credential = new DefaultAzureCredential();
                var client = new SecretClient(new Uri(keyVaultUri), credential);

                builder.Configuration.AddAzureKeyVault(client, new KeyVaultSecretManager());
            }

            builder.Services.AddDbContext<ProductAppDbContext>(
                options => options.UseSqlServer(builder.Configuration["connection-string"]));

            #endregion

            #region Services
            builder.Services.AddScoped<IProductService,ProductServiceBL>();
            #endregion

            #region Repository
            builder.Services.AddScoped<IRepository<int,Product>, ProductRepository>();
            #endregion

            #region CORS
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("MyCors", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #endregion


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseCors("MyCors");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
