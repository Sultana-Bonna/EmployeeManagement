using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace jan6_19_test.Models
{
    public class TestDbContext:DbContext
    {
        public TestDbContext()
            : base("name=TestDbContext")
        {
           // Database.SetInitializer<TestDbContext>(new CreateDatabaseIfNotExists<TestDbContext>());
            //Database.SetInitializer<TestDbContext>(null);
        }
        public static TestDbContext Create()
        {
            return new TestDbContext();
        }
       
        public virtual DbSet<TestInfo> TextInfoDbSet { get; set; } //note:error spelling
        public virtual DbSet<Text> TextDbSet { get; set; }
        public virtual DbSet<Employee> EmployeeDbSet { get; set; }

        public virtual DbSet<MultiImage> MultiImageDbSet { get; set; }
        public System.Data.Entity.DbSet<jan6_19_test.Models.TextVM> TextVMs { get; set; }
        /*public System.Data.Entity.DbSet<jan6_19_test.Models.ViewModels.ImageVM> ImageVMs { get; set; }*/
    }
}