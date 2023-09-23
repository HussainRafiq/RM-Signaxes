using RM_Signaxes.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RM_Signaxes
{
    public class RM_Model : DbContext
    {
        // Your context has been configured to use a 'RM_Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RM_Signaxes.RM_Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RM_Model' 
        // connection string in the application configuration file.
        public RM_Model()
            : base("name=RM-Model")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<OperatorsJobs> OperatorsJobs { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperatorsJobs>().Property(t => t.TotalActualSpentHours)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            
            base.OnModelCreating(modelBuilder);
        }
    }
    

}