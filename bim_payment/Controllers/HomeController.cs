using bim_payment.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace bim_payment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(long id)
        {

            ViewBag.id = id;
            return View();

        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomStringNumber()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, false));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public ActionResult PagarViaMasterCard(string SessioId, long id_produto)
        {
            string status = "";
            string orderID = RandomStringNumber();
            string transationID = RandomStringNumber();

            RestClient restClient = new RestClient("https://test-gateway.mastercard.com");
            RestRequest authorizationRequest = new RestRequest("/api/rest/version/54/merchant/22599/order/" + orderID + "/transaction/" + transationID, Method.PUT);
            authorizationRequest.AddHeader("Content-Type", "application/json");
            authorizationRequest.AddHeader("Authorization", $"Basic bWVyY2hhbnQuMjI1OTk6MzBlYzZkOGY1MmI3YzJiY2YzOGZkNWZlNzRjNDhkMzA=");

            var data = new PayRequest() { apiOperation = "AUTHORIZE", session = new PayRequest.Session() { id = SessioId }, sourceOfFunds = new PayRequest.SourceOfFunds() { type = "CARD" }, order = new PayRequest.Order() { amount = 100, currency = "MZN" } };
            authorizationRequest.AddJsonBody(data);
            var response = restClient.Execute<PayResponse>(authorizationRequest);
            PayResponse Autorizacao = response.Data;
            
            if(Autorizacao.order.status== "AUTHORIZED")
            {
                RestRequest CaptureRequest = new RestRequest("/api/rest/version/54/merchant/22599/order/" + Autorizacao.order.id + "/transaction/" + RandomStringNumber(), Method.PUT);
                CaptureRequest.AddHeader("Content-Type", "application/json");
                CaptureRequest.AddHeader("Authorization", $"Basic bWVyY2hhbnQuMjI1OTk6MzBlYzZkOGY1MmI3YzJiY2YzOGZkNWZlNzRjNDhkMzA=");

                var data2 = new PayRequest() { transaction = new PayRequest.Transaction() { amount = 100, currency = "MZN" }, apiOperation = "CAPTURE", session = new PayRequest.Session() { id = SessioId }, sourceOfFunds = new PayRequest.SourceOfFunds() { type = "CARD" } };
                CaptureRequest.AddJsonBody(data2);
                var captura = restClient.Execute<PayResponse>(CaptureRequest);
                PayResponse capturacao = captura.Data;

                if (capturacao.order.status == "CAPTURED")
                {
                     status = "CAPTURED";
                }
            }


            if (status == "CAPTURED")
            {
                return Redirect("https://www.mussika.co.mz/payment/compra?id_produto=" + id_produto + "&oque_e=musica");
                
            }
            else
            {
                return Redirect("/PubMusica/PagamentoFalhou?erro=sads");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}