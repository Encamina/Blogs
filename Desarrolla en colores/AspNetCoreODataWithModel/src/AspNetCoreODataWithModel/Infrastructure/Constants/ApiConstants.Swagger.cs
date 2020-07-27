namespace AspNetCoreODataWithModel.Infrastructure.Constants
{
    /// <summary>
    /// ApiConstants
    /// </summary>
    public static partial class ApiConstants
    {
        /// <summary>
        /// ContentType constants
        /// </summary>
        public static class Swagger
        {
            /// <summary>
            /// Swagger endpoint
            /// </summary>
            public const string Endpoint = "/api-docs/v1/swagger.json";
            /// <summary>
            /// Swagger Api Name
            /// </summary>
            public const string ApiName = "AspNetCoreODataWithModel Api Example";
            /// <summary>
            /// Swagger Api Version
            /// </summary>
            public const string ApiVersion = "v1";
            /// <summary>
            /// Swagger route
            /// </summary>
            public const string RouteTemplate = "api-docs/{documentName}/swagger.json";
            /// <summary>
            /// Swagger route prefix
            /// </summary>
            public const string RoutePrefix = "swagger";
        }
    }
}
