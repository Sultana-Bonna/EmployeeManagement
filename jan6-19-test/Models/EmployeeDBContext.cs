using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace jan6_19_test.Models
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
            : base("name=EmployeeDBContext")
        {

        }

        public virtual DbSet<Employee> EmployeeDbSet { get; set; }
        public virtual DbSet<TestInfo> EmployeeImageDbSet { get; set; }
    }
}