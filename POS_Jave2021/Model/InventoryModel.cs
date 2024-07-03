using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Jave2021.Model
{
    public class InventoryModel
    {
        public int ID { get; set; }
        public string inv_id { get; set; }
        public string product_id { get; set; }
        public decimal price_cost { get; set; }
        public decimal selling_price { get; set; }
        public DateTime cdt { get; set; }
        public DateTime udt { get; set; }
        public int overall_inv { get; set; }
        public int sold_inv { get; set; }
        public int available_inv { get; set; }
    }
}
