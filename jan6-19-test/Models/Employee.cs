using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jan6_19_test.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(250)]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        //[Required,StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(15)]
        public string ContactNo { get; set; }
        [Required, StringLength(19)]
        public string NID { get; set; }
        public string BloodGroup { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        [StringLength(25)]
        public string Addresses { get; set; }
        /*public List<Address> Addresses { get; set; }*/
        [ForeignKey("EmployeeImage")]
        public int? EmployeeImageId { get; set; }
        public TestInfo EmployeeImage { get; set; }


        /*[ForeignKey("EmployeeImage")]
        public int? EmployeeImageId { get; set; }
        public EmployeeImage EmployeeImage { get; set; }*/
        public bool IsDelete()
        {
            return false;
        }
    }
}