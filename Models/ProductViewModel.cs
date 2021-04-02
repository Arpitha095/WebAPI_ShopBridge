using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_ShopBridge.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<double> Price { get; set; }
        

    }
}