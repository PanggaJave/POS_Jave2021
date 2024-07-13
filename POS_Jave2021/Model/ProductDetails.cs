using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Jave2021.Model
{
    public class ProductDetails
    {
        public int ID { get; set; }
        public string item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set;}
        public int item_catigory { get; set; }
        public DateTime cdt { get; set; }
        public DateTime udt { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string item_img { get; set; }
    }
}
