using ReservationPayment.Models;
using ReservationPayment.Models.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Web.Mvc;

namespace ReservationPayment.Controllers
{
    public class PaymentController : Controller
    {
        private IRailwayRepository<Reservation>  ResObj;
        public PaymentController()
        {
            this.ResObj = new RailwayRepository<Reservation>();

        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateOrder(Models.OrderModel _requestData)
        {
            
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
            Session["transactionId"] = transactionId;   

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_aJVAJtaSskH7US", "5ug92ZJIU89n0R3SKU7O0RP8");
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", 500 * 100);  
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); 
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
           // string orderId = orderResponse["id"].ToString();

            Models.OrderModel orderModel = new Models.OrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_aJVAJtaSskH7US",
                amount = 500 * 100,
                currency = "INR",
                name = _requestData.name,
                email = _requestData.email,
                contactNumber = _requestData.contactNumber,
                address = _requestData.address,
              
            };

            return View("PaymentPage", orderModel);
        }

  


        [HttpPost]
        public ActionResult Complete(Reservation reservation)
        {

           
           

            string paymentId = Request.Params["rzp_paymentid"];

          
            string orderId = Request.Params["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_aJVAJtaSskH7US", "5ug92ZJIU89n0R3SKU7O0RP8");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

          

            if (paymentCaptured.Attributes["status"] == "captured")
            {

              

                Guid guid = Guid.NewGuid();

                BigInteger big = new BigInteger(guid.ToByteArray());
                var pnr = big.ToString().Substring(0, 10);

                var str = pnr.Replace("-", string.Empty);

                Session["PNR"]= str;

                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }
    }
}