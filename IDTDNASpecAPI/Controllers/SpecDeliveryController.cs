using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using BusinessServices;
using AttributeRouting.Web;
using AttributeRouting.Web.Mvc;
using System.Dynamic;
using System.Data;
using IDTDNASpecAPI.ErrorHelper;

namespace IDTDNASpecAPI.Controllers
{
    public class SpecDeliveryController : ApiController
    {

        #region Private variable.

        private readonly ISpecDeliverySevices _deliveryServices;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public SpecDeliveryController(ISpecDeliverySevices deliveryServices)
        {
            _deliveryServices = deliveryServices;
        }

        #endregion

        [AcceptVerbs("POST", "GetAllSpecDeliveries")]
        public HttpResponseMessage GetAllSpecDeliveries()
        {
            var specdeliveries = _deliveryServices.GetAllSpecDeliveries();
            var specdeliveryEntities = specdeliveries as List<Spec_DeliveryEntity> ?? specdeliveries.ToList();
            if (specdeliveryEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, specdeliveryEntities);

            throw new ApiDataException(404, "No deliveries found.", HttpStatusCode.NotFound);

            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [AcceptVerbs("POST", "GetSpecDeliveriesByFilter")]
        public HttpResponseMessage GetSpecDeliveriesByFilter(List<Spec_Input> specInputs)
        {
            DataTable specdeliveries = _deliveryServices.GetSpecDeliveriesByFilter(specInputs);
            if (specdeliveries.Rows.Count> 0)
                return Request.CreateResponse(HttpStatusCode.OK, specdeliveries);

            throw new ApiDataException(404, "No data found for the input(s) ", HttpStatusCode.NotFound);
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [AcceptVerbs("POST", "GetSpecDeliveryById")]
        public HttpResponseMessage GetSpecDeliveryById(int id)
        {

            var product = _deliveryServices.GetSpecDeliveryById(id);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            throw new ApiDataException(404, "No delivery found for the id "+id, HttpStatusCode.NotFound);
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [AcceptVerbs("POST", "CreateSpecDelivery")]
        public int CreateSpecDelivery([FromBody] Spec_DeliveryEntity specDeliveryEntity)
        {
            return _deliveryServices.CreateSpecDelivery(specDeliveryEntity);
        }


        [PUT("Update/productid/{id}")]
        [PUT("Modify/productid/{id}")]
        public bool Put(int id, [FromBody] Spec_DeliveryEntity specDeliveryEntity)
        {
            if (id > 0)
            {
                return _deliveryServices.UpdateSpecDelivery(id, specDeliveryEntity);
            }
            return false;
        }

        // DELETE api/product/5
        [AcceptVerbs("POST","CANCEL")]
        public bool CancelSpecDelivery(List<int> ids)
        {
            if (ids.Count() > 0)
                return _deliveryServices.CancelSpecDelivery(ids);

            return false;
        }

    }
}
