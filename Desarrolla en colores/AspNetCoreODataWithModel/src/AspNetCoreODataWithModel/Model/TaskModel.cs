using System;

namespace AspNetCoreODataWithModel.Model
{
    /// <summary>
    /// TaskModel
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Observations
        /// </summary>
        public string Observations { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Billable
        /// </summary>
        public bool Billable { get; set; }
    }
}
