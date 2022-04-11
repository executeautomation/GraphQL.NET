using System.Text;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using GraphQLProductApp.GraphQL;
using GraphQLProductApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GraphQLProductApp;

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
                        new OpenApiInfo
                        {
                            Title = "GraphQLProductApp",
                            Version = "v1"
                        });
                c.OperationFilter<SwaggerFileOperationFilter>();
                // To Enable authorization using Swagger (JWT)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
            };
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
            .AddGraphTypes(typeof(DataSchema), ServiceLifetime.Scoped);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        ProductDbContext productDbContext
    )
    {
        if (env.IsDevelopment() || env.IsProduction())
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
        app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

        app
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseAuthentication();
        productDbContext.Seed();
    }
}