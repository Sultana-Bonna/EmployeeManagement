using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jan6_19_test.Models
{
    public class Text
    {   [Key]   
        public int TextId { get; set; }

        public string TextContent { get; set; }

    }
}