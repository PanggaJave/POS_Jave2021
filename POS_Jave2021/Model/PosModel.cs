using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Jave2021.Model
{
    public class PosModel
    {
        public int ID { get; set; }
        public string pos_id { get; set; }
        public string total_inv { get; set; }
        public decimal total_price { get; set; }
        public decimal cash { get; set; }
        public decimal change { get; set; }
        public string user_id { get; set; }
    }

    public class PosSaleReportModel
    {
        public decimal Cash { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set;}
        public bool IsCancel { get; set; }
        public int QTY { get; set; }
        public DateTime tdt { get; set; }
        public bool IsDebtCredit { get; set; }
        public string Remarks { get; set; }
        public string UserID { get; set; }
    }

    public class PosInvSoldModel
    {    
        public string ProductID { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int QTY { get; set; }
        public DateTime tdt { get; set; }
        public bool IsCancel { get; set; }
        public string Remarks { get; set; }
    }
}
