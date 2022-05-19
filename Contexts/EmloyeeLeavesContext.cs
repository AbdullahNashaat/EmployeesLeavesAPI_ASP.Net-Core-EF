using EmployeesLeavesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql;
using System.Configuration;

namespace EmployeesLeavesAPI.Contexts
{
    public class EmloyeeLeavesContext : DbContext
    {
        //public EmloyeeLeavesContext(DbContextOptions<EmloyeeLeavesContext> options)
        // : base(options)
        //{
        //    // Creates the database if not exists
        //   // Database.EnsureCreated();

        //}
        public EmloyeeLeavesContext()
        {
            // Creates the database if not exists
            //Database.EnsureCreated();
           // Database.SetInitializer<EmloyeeLeavesContext>(new CreateDatabaseIfNotExists<EmloyeeLeavesContext>());

        }
        //private IConfiguration Configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql(connectionString: Configuration. GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(8, 0, 29)));
            //optionsBuilder.UseMySql(connectionString: Configuration.GetValue<string>("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29)));
            optionsBuilder.UseMySql(connectionString: @"server=localhost;database=EmloyeeLeaves;user=root;password=root;", new MySqlServerVersion(new Version(8, 0, 29)));

            //mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id=1, Name= "Casual", AnnualLimit = 7 },
                 new LeaveType { Id = 2, Name = "Schedual", AnnualLimit = 14 }
                );
        }

        //}
        public DbSet<EmployeeLeave> EmployeesLeaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<EmployeeLeaveDetail> EmployeeLeaveDetails { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;database=EmloyeeLeaves;uid=root;password=root;");
        //     //optionsBuilder.UseMySql(@"server=localhost;database=BookStoreDb;uid=root;password=;");
        //}

    }
}
