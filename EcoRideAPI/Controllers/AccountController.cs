using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EcoRideAPI.Controllers
{
   
    public class AccountController : ApiController
    {
        public class StripeApiKeysAndInfo
        {
            //EcorideDev
            public static string sk = "sk_test_yQ6K5DhDlYNe2B4B8S2LeF6p00O6is4ABU";
            public static string pk = "pk_test_6D3lQgUeFJBIDIGnf9Idyat700LTzRcLsq";
        }
    }
}