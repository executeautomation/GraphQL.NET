using GraphQLProductApp.Data;
using GraphQLProductApp.GraphQL;
using GraphQLProductApp.Repository;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using GraphQL.Server.Ui.Playground;

namespace GraphQLProductApp
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
            services.AddControllers();
            services
                .AddSwaggerGen(c =>
                {
                    c
                        .SwaggerDoc("v1",
                        new OpenApiInfo {
                            Title = "GraphQLProductApp",
                            Version = "v1"
                        });
                });

            services
                .AddDbContext<ProductDbContext>(option =>
                    option.UseSqlite(@"Data Source=Product.db"));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IComponentRepository, ComponentRepository>();

            services.AddScoped<DataSchema>();

            services
                .AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof (DataSchema), ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ProductDbContext productDbContext
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app
                    .UseSwaggerUI(c =>
                        c
                            .SwaggerEndpoint("/swagger/v1/swagger.json",
                            "GraphQLProductApp v1"));

                productDbContext.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseGraphQL<DataSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            productDbContext.Seed();
        }
    }
}
