using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;

namespace IDTDNASpecAPI.Controllers
{
    public class SpecController : ApiController
    {

        #region Private Variable
        private readonly ISpecServices _specServices;

        #endregion

        #region Constructor


        public SpecController(ISpecServices specServices)
        {
            _specServices = specServices;
        }

        #endregion

        // GET: api/SpecDelivery

        [HttpPost]
        public HttpResponseMessage Get( Spec_Input input )
        {
            object o=new object();
            o = _specServices.GetAllSpecs(input);
            return Request.CreateResponse(HttpStatusCode.OK, o);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        // GET: api/SpecDelivery/5(
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SpecDelivery
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SpecDelivery/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SpecDelivery/5
        public void Delete(int id)
        {
        }
    }
}
