using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace caTest
{
    class User
    {
        // private method
        private string PrivateMethod()
        {
            return "Private method executed !!";
        }
    }
 
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            //var pocoObject = new
            //{
            //    mpxn = "7828042700" 
            //};

            ////Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            //string json = JsonConvert.SerializeObject(pocoObject);

            ////Needed to setup the body of the request
            //StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            ////The url to post to.
            //var url = "https://localhost/requests/power/registration-deactivation/7828042700";
            //var client = new HttpClient();



            var promotionEng = new PromotionEngin();
            promotionEng.start();


            //var refl = new Reflection();
            //refl.run();




        }
 
    }
}
