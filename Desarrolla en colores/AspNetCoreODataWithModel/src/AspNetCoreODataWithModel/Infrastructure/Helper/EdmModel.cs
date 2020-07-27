using AspNetCoreODataWithModel.Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;

namespace AspNetCoreODataWithModel.Infrastructure.Helper
{
    /// <summary>
    /// EdmModel
    /// </summary>
    public static class EdmModelHelper
    {
        /// <summary>
        /// GetEdmModel
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder(serviceProvider);
            return GetEdmModel(builder);
        }

        /// <summary>
        /// GetEdmModel
        /// </summary>
        /// <returns></returns>
        public static IEdmModel GetEdmModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder().EnableLowerCamelCase(NameResolverOptions.ProcessReflectedPropertyNames | NameResolverOptions.ProcessExplicitPropertyNames);
            return GetEdmModel(builder);
        }

        private static IEdmModel GetEdmModel(ODataModelBuilder builder)
        {
            EntitySetConfiguration<Tarea> tasks = builder.EntitySet<Tarea>("Tareas");
            builder.ContainerName = "DefaultContainer";
            tasks.EntityType.Name = "Tarea";
            tasks.EntityType.Namespace = "AspNetCoreODataWithModel.Data.Entities";

            tasks.EntityType.Property(p => p.Id).Name = "Id";
            tasks.EntityType.Property(p => p.Nombre).Name = "Name";
            tasks.EntityType.Property(p => p.Fecha).Name = "Date";
            tasks.EntityType.Property(p => p.Facturable).Name = "Billable";
            tasks.EntityType.Property(p => p.Observaciones).Name = "Observations";

            tasks.EntityType
                            .Filter()
                            .Count()
                            .Expand()
                            .OrderBy()
                            .Page()
                            .Select();

            return builder.GetEdmModel();
        }
    }
}
