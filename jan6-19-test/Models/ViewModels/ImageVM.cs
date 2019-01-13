using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using AutoMapper;

namespace jan6_19_test.Models.ViewModels
{
    public class ImageVM
    {

        [StringLength(1000)]
        public string ImageName { get; set; }

        [Key]
        public int ImageId { get; set; }


        [StringLength(1000)]
        public string ImagePath { get; set; }
        public byte[] ImageByte { get; set; }

        [NotMapped]
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}