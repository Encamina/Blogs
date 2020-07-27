using AspNetCoreODataWithModel.Data;
using AspNetCoreODataWithModel.Data.Entities;
using AspNetCoreODataWithModel.Data.Mapper;
using AspNetCoreODataWithModel.Infrastructure.Constants;
using AspNetCoreODataWithModel.Infrastructure.Extensions;
using AspNetCoreODataWithModel.Infrastructure.Helper;
using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;

namespace AspNetCoreODataWithModel
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private const string AllowAllOriginsPolicy = "AllowAllOrigins";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Environment
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            
            services.AddLogging()
                    .AddOptions()
                    .AddAutoMapper(config => config.AddProfile<MappingProfile>(),
                                                AppDomain.CurrentDomain.GetAssemblies())
                    .AddCustomOData()
                    .AddCors(AllowAllOriginsPolicy)
                    .AddCustomApiVersioning()
                    .AddSwagger()
                    .AddProblemDetails()
                    .AddCustomDbContext()
                    .AddDependencyInjection();

            services.AddControllers()
                .AddJsonSerializerSettings()
                .AddApiBehaviorOptions();

            var context = services.BuildServiceProvider().GetService<ApplicationContext>();
            AddTestData(context);
            services.AddMvc(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                options.EnableEndpointRouting = false;
                // Workaround: https://github.com/OData/WebApi/issues/1177
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(ApiConstants.ContentTypes.ApplicationOData));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue(ApiConstants.ContentTypes.ApplicationOData));
                }
            });              
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = ApiConstants.Swagger.RouteTemplate;
            });
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = ApiConstants.Swagger.ApiName;
                c.SwaggerEndpoint(ApiConstants.Swagger.Endpoint, $"{ApiConstants.Swagger.ApiName} {ApiConstants.Swagger.ApiVersion}");
                c.RoutePrefix = ApiConstants.Swagger.RoutePrefix;
                c.DocExpansion(DocExpansion.None);
            });

            app.UseAuthorization(); 

            app.UseProblemDetails()
               .UseMvc(routeBuilder =>
               {
                   routeBuilder.EnableDependencyInjection();
                   routeBuilder.Select().Filter().OrderBy().Expand().Count().MaxTop(null);

                   routeBuilder.MapODataServiceRoute("api", "api", EdmModelHelper.GetEdmModel(app.ApplicationServices));
               });

            
        }

        private static void AddTestData(ApplicationContext context)
        {
            var task1 = new Tarea
            {
                Id = 1,
                Nombre = "Tarea 1",
                Observaciones = "Sin observaciones",
                Facturable = true,
                Fecha = DateTime.UtcNow.AddDays(-3)
            };

            var task2 = new Tarea
            {
                Id = 2,
                Nombre = "Tarea 2",
                Observaciones = "Sin observaciones",
                Facturable = false,
                Fecha = DateTime.UtcNow
            };

            var task3 = new Tarea
            {
                Id = 3,
                Nombre = "Tarea 3",
                Observaciones = "Esto es una prueba de observaciones",
                Facturable = true,
                Fecha = DateTime.UtcNow.AddDays(-7)
            };

            context.AddRange(task1, task2, task3);
            context.SaveChanges();
        }
    }
}
