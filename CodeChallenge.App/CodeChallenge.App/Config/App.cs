using System;
using System.IO;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using CodeChallenge.Repos;
using CodeChallenge.Repos.DynamoDb;
using CodeChallenge.Repos.FileRepo;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeChallenge.App.Config
{
    public class App
    {
        public WebApplication Configure(string[] args)
        {
            args ??= Array.Empty<string>();

            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
            var config = configBuilder.Build();


            var builder = WebApplication.CreateBuilder(args);
            var awsOptions = builder.Configuration.GetAWSOptions();
            builder.UseEmployeeDB();


            builder.Services.AddDefaultAWSOptions(awsOptions);
            builder.Services.AddAWSService<IAmazonDynamoDB>();
            builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
            builder.Services.AddScoped<ICompensationRepo, DynamoCompensationRepo>();
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRespository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddControllers();

            var app = builder.Build();
            var env = builder.Environment;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedEmployeeDB();
            }
            app.UseAuthorization();
            app.MapControllers();           

            return app;
        }

        private void SeedEmployeeDB()
        {
            new EmployeeDataSeeder(
                new EmployeeContext(
                    new DbContextOptionsBuilder<EmployeeContext>().UseInMemoryDatabase("EmployeeDB").Options
            )).Seed().Wait();
        }
    }
}
