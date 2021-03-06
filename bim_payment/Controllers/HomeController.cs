﻿using bim_payment.Models;
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
        public ActionResult Index(long id, decimal valor,string oque_e, string checkIn="", string checkOut="", bool tem_entrega = false, int id_transportadora = 0, string local_entrega = "", string nome_destinatario = "", string contacto_destinatario = "")
        {

            ViewBag.id = id;
            ViewBag.valor = valor;
            ViewBag.oque_e = oque_e;
            ViewBag.checkIn = checkIn;
            ViewBag.checkOut = checkOut;
            ViewBag.tem_entrega = tem_entrega;
            ViewBag.id_transportadora = id_transportadora;
            ViewBag.local_entrega = local_entrega;
            ViewBag.nome_destinatario = nome_destinatario;
            ViewBag.contacto_destinatario = contacto_destinatario;
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
        public ActionResult PagarViaMasterCard(string SessioId, long id_produto,decimal valor,string oque_e, string checkIn = "", string checkOut = "", bool tem_entrega = false, int id_transportadora = 0, string local_entrega = "", string nome_destinatario = "", string contacto_destinatario = "")
        {
            try
            {
                string status = "FAILED";
                string orderID = RandomStringNumber();
                string transationID = RandomStringNumber();

                RestClient restClient = new RestClient("https://millenniumbim.gateway.mastercard.com");
                RestRequest authorizationRequest = new RestRequest("/api/rest/version/54/merchant/22599/order/" + orderID + "/transaction/" + transationID, Method.PUT);
                authorizationRequest.AddHeader("Content-Type", "application/json");
                authorizationRequest.AddHeader("Authorization", $"Basic bWVyY2hhbnQuMjI1OTk6YzQwZjE1MTQ0MmQ3ZmViYWQ2MDZmZDY1YjFkNjYzZDY=");

                var data = new PayRequest() { apiOperation = "AUTHORIZE", session = new PayRequest.Session() { id = SessioId }, sourceOfFunds = new PayRequest.SourceOfFunds() { type = "CARD" }, order = new PayRequest.Order() { amount = valor, currency = "MZN" } };
                authorizationRequest.AddJsonBody(data);
                var response = restClient.Execute<PayResponse>(authorizationRequest);
                PayResponse Autorizacao = response.Data;

               if(Autorizacao.result== "ERROR")
                {
                    //return Redirect("https://localhost:44360/PubMusica/PagamentoFalhou?erro=Houve um erro inesperado, volte a tentar mais tarde ou conatcte o administrador. Codigo do Erro: APIPAY01");
                    return Redirect("https://www.mussika.co.mz/PubMusica/PagamentoFalhou?erro=Houve um erro inesperado, volte a tentar mais tarde ou conatcte o administrador. Codigo do Erro: APIPAY01");

                }
                else
                {
                    if (Autorizacao.order.status == "AUTHORIZED")
                    {
                        RestRequest CaptureRequest = new RestRequest("/api/rest/version/54/merchant/22599/order/" + Autorizacao.order.id + "/transaction/" + RandomStringNumber(), Method.PUT);
                        CaptureRequest.AddHeader("Content-Type", "application/json");
                        CaptureRequest.AddHeader("Authorization", $"Basic bWVyY2hhbnQuMjI1OTk6YzQwZjE1MTQ0MmQ3ZmViYWQ2MDZmZDY1YjFkNjYzZDY=");

                        var data2 = new PayRequest() { transaction = new PayRequest.Transaction() { amount = valor, currency = "MZN" }, apiOperation = "CAPTURE", session = new PayRequest.Session() { id = SessioId }, sourceOfFunds = new PayRequest.SourceOfFunds() { type = "CARD" } };
                        CaptureRequest.AddJsonBody(data2);
                        var captura = restClient.Execute<PayResponse>(CaptureRequest);
                        PayResponse capturacao = captura.Data;

                        if (capturacao.order.status == "CAPTURED")
                        {
                            status = "CAPTURED";
                        }
                    }
                }
                


                   


                if (status == "CAPTURED")
                {
                    return Redirect("https://www.mussika.co.mz/payment/compra?id_produto=" + id_produto + "&valor=" + valor + "&oque_e=" + oque_e + "&checkIn="+checkIn + "&checkOut="+checkOut+ "&tem_entrega ="+ tem_entrega+ "&id_transportadora ="+ id_transportadora + "&local_entrega=" + local_entrega + "&nome_destinatario=" + nome_destinatario + "&contacto_destinatario=" + contacto_destinatario);
                    //return Redirect("https://localhost:44360/payment/compra?id_produto=" + id_produto + "&oque_e=" + oque_e);

                }
                else
                {

                    return Redirect("https://www.mussika.co.mz/PubMusica/PagamentoFalhou?erro="+status);
                    //return Redirect("https://localhost:44360/PubMusica/PagamentoFalhou?erro=" + status);
                }
            }
            catch (Exception)
            {

                return Redirect("https://www.mussika.co.mz/PubMusica/PagamentoFalhou?erro=Houve um erro inesperado, volte a tentar mais tarde ou conatcte o administrador. Codigo do Erro: APIPAY-UNKNOW");
                //return Redirect("https://localhost:44360/PubMusica/PagamentoFalhou?erro=Houve um erro inesperado, volte a tentar mais tarde ou conatcte o administrador. Codigo do Erro: APIPAY01");
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