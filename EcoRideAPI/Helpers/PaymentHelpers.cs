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
    }
}