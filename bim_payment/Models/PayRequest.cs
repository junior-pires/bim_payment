using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bim_payment.Models
{
    public class PayRequest
    {
        public string apiOperation { get; set; }
       
        public Order order { get; set; }
        public Transaction transaction { get; set; }
        public SourceOfFunds sourceOfFunds { get; set; }
        public Session session { get; set; }

        public class Order
        {
           public string currency { get; set; }
            public string id { get; set; }
            public decimal amount { get; set; }
        }
        public class Transaction
        {
            public string currency { get; set; }
            public decimal amount { get; set; }
        }
        public class SourceOfFunds
        {
            public string type { get; set; }
            
        }
        public class Session
        {
            public string id { get; set; }

        }
    }

}