using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace azureDeduplicate.Controllers
{
    public class StringController : ApiController
    {
        public string Get()
        {
            return "Hellow, World!";
        }
    }


}
