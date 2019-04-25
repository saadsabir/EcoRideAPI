using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stripe;

namespace EcoRideAPI.Helpers
{
    public class PaymentHelpers
    {
        //CALCULER LES FRAIS STRIPE SELON LE TYPE DE LA CARTE
        public static int fraisStripe(int montantLocation, string sourceCountry)
        {
            const double POURCENTAGESTRIPECBNONEUROP = 0.029;//2.9%
            const double POURCENTAGESTRIPECBEUROP = 0.014;//1.2%
            const int STRIPETARIFS = 25;
            if (sourceCountry == "MX" || sourceCountry == "US" || sourceCountry == "DK" || sourceCountry == "GB" || sourceCountry == "NO" || sourceCountry == "SE" || sourceCountry == "CH" || sourceCountry == "CA" || sourceCountry == "BR" || sourceCountry == "AU" || sourceCountry == "CN" || sourceCountry == "HK" || sourceCountry == "JP" || sourceCountry == "NZ" || sourceCountry == "SG" || sourceCountry == "RU")
            {
                int commissionStripe = Convert.ToInt32((montantLocation * POURCENTAGESTRIPECBNONEUROP) + STRIPETARIFS);
                return commissionStripe;
            }
            else
            {
                if (montantLocation <= 500)//if amount inferieur ou égale à 5€
                {//Amount <= 5€  ==> fee = 5% + 0,05 / transaction
                    int commissionStripe = Convert.ToInt32((montantLocation * 0.05) + 5);
                    return commissionStripe;
                }
                else
                {
                    int commissionStripe = Convert.ToInt32((montantLocation * POURCENTAGESTRIPECBEUROP) + STRIPETARIFS);
                    return commissionStripe;
                }

            }
        }

        //MONTANT EN CENTIME 10€ = 1000
        public static Charge chargePayementStripe(string secret_key, string customerID, int montant, string description_, string etablissementStripeAccountID, string cardSourceCountry, string cardSourceID)
        {
            try
            {
                StripeConfiguration.SetApiKey(secret_key);
                var frais_stripe = fraisStripe(montant, cardSourceCountry);
                var options = new ChargeCreateOptions
                {
                    Amount = montant,
                    Currency = "eur",
                    Description = description_,
                    CustomerId = customerID,
                    Capture = false,
                    SourceId = cardSourceID,
                    //SourceTokenOrExistingSourceId = cardSourceID,
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                return charge;
            }
            catch (StripeException e)
            {
                return null;
            }

        }

        //RETRIEVE A CHARGE
        public static Charge retrieveChargePaymentStripe(string secret_key, string idcharge)
        {
            try
            {
                StripeConfiguration.SetApiKey(secret_key);
                var service = new ChargeService();
                var charge = service.Get(idcharge);
                return charge;

            }
            catch (StripeException e)
            {
                return null;
            }
        }

        //CAPTURE LIST OF CHARGE
        public static List<Charge> capturePayements(string secretKey, List<string> idPayement)
        {
            try
            {
                //get list of source from DB
                if (idPayement != null)
                {
                    StripeConfiguration.SetApiKey(secretKey);
                    var service = new ChargeService();
                    Charge charge = new Charge();
                    List<Charge> listResultCharge = new List<Charge>();
                    foreach (var t in idPayement)
                    {
                        try
                        {
                            DateTime dateNow = DateTime.Now;
                            charge = service.Capture(t, null);
                            //todo : dateCapture=now in Payment Table (DB part)
                        }
                        catch (StripeException stripeEx)
                        {
                            return null;
                        }
                        listResultCharge.Add(charge);
                    }
                    return listResultCharge;
                        
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}