using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace AuthorizationServer.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : ApiController
    {
        [HttpGet]
        public OkResult Get()
        {
            return Ok();
        }
    }
}
