using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jan6_19_test.Models
{
    public class MultiImage
    {

        [StringLength(1000)]
        public string ImageName { get; set; }
        [Key]
        public int ImageId { get; set; }
        public byte[] ImageByte { get; set; }

        [StringLength(1000)]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}