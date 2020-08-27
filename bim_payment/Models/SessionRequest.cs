using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bim_payment.Models
{
    public class SessionRequest
    {
        public string apiOperation { get; set; }
        public Interaction interaction { get; set; }
        public Order order { get; set; }
        public class Interaction
        {
            public string operation { get; set; }
            public string sessionId { get; set; }
            
        }

        public class Order
        {
            public string currency { get; set; }
            public string id { get; set; }
            public int amount { get; set; }
        }

           
        
    }
}