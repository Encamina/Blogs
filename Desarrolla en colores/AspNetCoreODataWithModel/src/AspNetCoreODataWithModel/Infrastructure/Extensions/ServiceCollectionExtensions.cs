using AspNetCoreODataWithModel.Data;
using AspNetCoreODataWithModel.Data.Repositories;
using AspNetCoreODataWithModel.Infrastructure.Constants;
using AspNetCoreODataWithModel.Infrastructure.Swagger;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;


namespace AspNetCoreODataWithModel.Infrastructure.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// AddCors
        /// </summary>
        /// <param name="services"></param>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public static IServiceCollection AddCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
                options.AddPolicy(policyName,
                    currentbuilder =>
                    {
                        currentbuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    })
                );

            return services;
        }

        /// <summary>
        /// AddCustomDbContext
        /// </summary>
        /// <param name="services"></param> 
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDataBase"));

            return services;
        }

        /// <summary>
        /// AddCustomOData
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomOData(this IServiceCollection services)
        {
            services.AddOData().EnableApiVersioning();
            services.AddODataQueryFilter();

            return services;
        }

        /// <summary>
        /// AddCustocmApiVersioning
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader(ApiConstants.ApiVersionHeader);
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }

        /// <summary>
        /// AddSwagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiConstants.Swagger.ApiVersion, new OpenApiInfo
                {
                    Version = ApiConstants.Swagger.ApiVersion,
                    Title = ApiConstants.Swagger.ApiName,
                    Description = ApiConstants.Swagger.ApiName
                });
                options.EnableAnnotations();
                options.IgnoreObsoleteProperties();
                options.IgnoreObsoleteActions();
                options.DescribeAllParametersInCamelCase();
                options.EnableAnnotations();
                options.OperationFilter<ODataQueryOptionsFilter>();  
               
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGen();

            return services;
        }

        /// <summary>
        /// AddDependencyInjection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
            return services;
        }
    }
}
