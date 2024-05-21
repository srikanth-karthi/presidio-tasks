
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RequestTrackerApp.Services;
using RequestTrackerApp.Context;
using RequestTrackerApp.Interface;
using RequestTrackerApp.Interfaces;
using RequestTrackerApp.Model;
using RequestTrackerApp.Repository;
using RequestTrackerApp.Service;
using System.Text;

namespace RequestTrackerApp
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


            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
        };

    });
            #region contexts
            builder.Services.AddDbContext<RequestTrackercontext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                );
            #endregion






            #region Service
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            #endregion



            #region
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRequestRepository>();
            builder.Services.AddScoped<IRepository<int, Requests>, RequestRepository>();
            builder.Services.AddScoped<IRepository<int, SolutionResposnse>, ResponseRepository>();
            builder.Services.AddScoped<IRepository<int, SolutionFeedback>, FeedbackRepository>();
            builder.Services.AddScoped<IRepository<int, RequestSolution>, SolutionsRepository>();

            #endregion



            builder.Services.AddHttpContextAccessor();







            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
