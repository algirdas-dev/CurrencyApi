using Currency.App.AppConfigs;
using Currency.DB;
using Currency.Domain.Dtos;
using Currency.Domain.IDtos;
using Currency.Domain.IHttpClients;
using Currency.Domain.IServices;
using Currency.Helpers.Connections;
using Currency.Infrastructure.HttpClients;
using Currency.Infrastructure.IRepositories;
using Currency.Infrastructure.Repositories;
using Currency.Infrastructure.Services;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;


namespace Currency.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettingsDto>(Configuration.GetSection("AppSettings"));

            services.AddControllers();
            services.AddMemoryCache();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { 
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<ProductContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddTransient<IDatabaseConnectionFactory>(e =>
            {
                return new SqlConnectionFactory(Configuration.GetConnectionString("sqlConnection"));
            });

            services.AddScoped<ICurrencyService, CurrencyService>()
                .AddScoped<ICurrencyRepository, CurrencyRepository>()
                .AddScoped<IRatesClient,RatesClient>()
                .AddScoped<MessageQueue>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.ConfigureCustomExceptionMiddleware();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
