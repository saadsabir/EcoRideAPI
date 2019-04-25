using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Generic;
using ChoppChoppWS.ModelsFront;
using System.Web.Http;
using EcoRideAPI.Helpers;
using Stripe;

namespace EcoRideAPI.Controllers
{
    public class RequestChargePayementStripe
    {
        //public string token { get; set; }
        public int montant { get; set; }
        public string description { get; set; }
        public string customerID { get; set; }
        public string accountID { get; set; }
        public string cardSourceCountry { get; set; }
        public string cardSourceID { get; set; }
    }
    public class PaymentController:ApiController
    {
        [HttpPost]
        [Route("api/v1/payment/chargePayementStripe")]
        public IHttpActionResult chargePayementStripe([FromBody] RequestChargePayementStripe request)
        {
            try
            {
                var charge = PaymentHelpers.chargePayementStripe(AccountController.StripeApiKeysAndInfo.sk, request.customerID, request.montant, request.description, request.accountID, request.cardSourceCountry, request.cardSourceID);
                if (charge != null)
                {
                    return Ok(new Response<Charge>()
                    {
                        Result = "SUCCESS",
                        commentaire = "Charge Success",
                        resultat = charge
                    });
                }

                return Ok(new Response<Charge>()
                {
                    Result = "REQUEST_FAILED",
                    commentaire = "une erreur est survenue",
                    resultat = null
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response<Charge>()
                {
                    Result = "REQUEST_FAILED",
                    commentaire = "une erreur est survenue",
                    resultat = null
                });
            }

        }
    }
}