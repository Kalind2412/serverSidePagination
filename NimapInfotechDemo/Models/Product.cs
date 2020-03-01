using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NimapInfotechDemo.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual Category Category { get; set; }
    }
}