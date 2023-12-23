using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping_System.Models
{
    public class Cart
    {
        public tbl_Product Product { get; set; }
        public int Quantity { get; set; }
    }
}