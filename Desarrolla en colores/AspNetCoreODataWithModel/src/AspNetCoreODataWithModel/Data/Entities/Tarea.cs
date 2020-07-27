using AspNetCoreODataWithModel.Shared.Models.Database;
using System;

namespace AspNetCoreODataWithModel.Data.Entities
{
    /// <summary>
    /// Tarea
    /// </summary>
    public class Tarea : BaseEntity
    {
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime? Fecha{ get; set; }
        /// <summary>
        /// Facturable
        /// </summary>
        public bool Facturable { get; set; }
    }
}
