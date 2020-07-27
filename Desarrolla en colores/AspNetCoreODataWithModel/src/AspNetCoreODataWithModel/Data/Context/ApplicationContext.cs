using AspNetCoreODataWithModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AspNetCoreODataWithModel.Data
{
    /// <summary>
    /// ApplicationContext
    /// </summary>
    public class ApplicationContext : DbContext
    { 
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
         
        /// <summary>
        /// Tareas
        /// </summary>
        public virtual DbSet<Tarea> Tareas { get; set; }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>().HasKey(t => new { t.Id });             
        }               
    }    
}
