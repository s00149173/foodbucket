using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodBucket.Models
{
    public class AddRecipieModel
    {
     
        public int id_country { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string ingredients { get; set; }
        public string preparation { get; set; }

        public  HttpPostedFileBase ImageRec { get; set; }
    
    }
}