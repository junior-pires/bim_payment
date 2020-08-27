using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bim_payment.Models
{
    public class SessionResponse
    {

        [JsonProperty("merchant", Required = Required.Always)]
        
        public long Merchant { get; set; }

        [JsonProperty("result", Required = Required.Always)]
        public string Result { get; set; }

        [JsonProperty("session", Required = Required.Always)]
        public Session Session { get; set; }

        [JsonProperty("successIndicator", Required = Required.Always)]
        public string SuccessIndicator { get; set; }
    }

    public partial class Session
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("updateStatus", Required = Required.Always)]
        public string UpdateStatus { get; set; }

        [JsonProperty("version", Required = Required.Always)]
        public string Version { get; set; }
    }
}
    
