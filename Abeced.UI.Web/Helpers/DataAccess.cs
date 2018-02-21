using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Abeced.UI.Web.Helpers
{
    public class DataAccess
    {


        public static HttpClient WebClient = new HttpClient();

        static DataAccess()
        {
            WebClient.BaseAddress = new Uri("http://localhost:20503/api/");
            WebClient.DefaultRequestHeaders.Clear();
            WebClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           

        }

    }
}