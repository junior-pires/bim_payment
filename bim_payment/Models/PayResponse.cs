using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bim_payment.Models
{
    public class PayResponse
    {
        public Device device { get; set; }
        public string gatewayEntryPoint { get; set; }
        public string merchant { get; set; }
        public Order order { get; set; }
        public Response response { get; set; }
        public string result { get; set; }
        public SourceOfFunds sourceOfFunds { get; set; }
        public DateTime timeOfRecord { get; set; }
        public Transaction transaction { get; set; }
        public string version { get; set; }

        public class Device
        {
            public string browser { get; set; }
            public string ipAddress { get; set; }
        }

        public class Chargeback
        {
            public int amount { get; set; }
            public string currency { get; set; }
        }

        public class Order
        {
            public double amount { get; set; }
            public Chargeback chargeback { get; set; }
            public DateTime creationTime { get; set; }
            public string currency { get; set; }
            public string id { get; set; }
            public string merchantCategoryCode { get; set; }
            public string status { get; set; }
            public double totalAuthorizedAmount { get; set; }
            public double totalCapturedAmount { get; set; }
            public double totalRefundedAmount { get; set; }
        }

        public class CardSecurityCode
        {
            public string acquirerCode { get; set; }
            public string gatewayCode { get; set; }
        }

        public class Response
        {
            public string acquirerCode { get; set; }
            public CardSecurityCode cardSecurityCode { get; set; }
            public string gatewayCode { get; set; }
        }

        public class Expiry
        {
            public string month { get; set; }
            public string year { get; set; }
        }

        public class Card
        {
            public string brand { get; set; }
            public Expiry expiry { get; set; }
            public string fundingMethod { get; set; }
            public string issuer { get; set; }
            public string nameOnCard { get; set; }
            public string number { get; set; }
            public string scheme { get; set; }
            public string storedOnFile { get; set; }
        }

        public class Provided
        {
            public Card card { get; set; }
        }

        public class SourceOfFunds
        {
            public Provided provided { get; set; }
            public string type { get; set; }
        }

        public class Acquirer
        {
            public string id { get; set; }
            public string merchantId { get; set; }
        }

        public class Transaction
        {
            public Acquirer acquirer { get; set; }
            public double amount { get; set; }
            public string currency { get; set; }
            public string id { get; set; }
            public string receipt { get; set; }
            public string source { get; set; }
            public string terminal { get; set; }
            public string type { get; set; }
        }
    }
}