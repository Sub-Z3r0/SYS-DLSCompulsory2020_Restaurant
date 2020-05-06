using System;
using CustomerAPI.Data;
using CustomerAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedModels;

namespace CustomerAPI
{
    public class Startup
    {
        // Base URL for the product service when the solution is executed using docker-compose.
        // The product service (running as a container) listens on this URL for HTTP requests
        // from other services specified in the docker compose file (which in this solution is
        // the order service).
        Uri productServiceBaseUrl = new Uri("http://productapi/products/");

        // RabbitMQ connection string (I use CloudAMQP as a RabbitMQ server).
        // Remember to replace this connectionstring with youur own.
        string cloudAMQPConnectionString =
            "host=kangaroo.rmq.cloudamqp.com;virtualHost=tpyrvtpo;username=tpyrvtpo;password=8neWlMBPVyuuxZuSCRtIMhjjz4IxGvoD";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In-memory database:
            services.AddDbContext<CustomerApiContext>(opt => opt.UseInMemoryDatabase("CustomersDb"));

            // Register repositories for dependency injection
            services.AddScoped<IRepository<Customer>, CustomerRepository>();

            // Register database initializer for dependency injection
            services.AddTransient<IDbInitializer, DbInitializer>();


            // Register product service gateway for dependency injection
            services.AddSingleton<IServiceGateway<ProductDto>>(new
                ProductServiceGateway(productServiceBaseUrl));

            // Register MessagePublisher (a messaging gateway) for dependency injection
            services.AddSingleton<IMessagePublisher>(new
                MessagePublisher(cloudAMQPConnectionString));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Initialize the database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                // Initialize the database
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<CustomerApiContext>();
                var dbInitializer = services.GetService<IDbInitializer>();
                dbInitializer.Initialize(dbContext);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
