using AttributeRouting.Web.Mvc;
using BusinessEntities;
using BusinessServices;
using IDTDNASpecAPI.ErrorHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IDTDNASpecAPI.Controllers
{
    public class SpecOligoController : ApiController
    {
        #region Private variable.

        private readonly ISpecOligoServices _oligoServices;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize oligo service instance
        /// </summary>
        public SpecOligoController(ISpecOligoServices oligoServices)
        {
            _oligoServices = oligoServices;
        }

        #endregion

        [AcceptVerbs("POST", "GetAllSpecOligos")]
        public HttpResponseMessage GetAllSpecOligos()
        {
            var specoligos = _oligoServices.GetAllSpecOligos();
            var specoligoEntities = specoligos as List<Spec_OligoEntity> ?? specoligos.ToList();
            if (specoligoEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, specoligoEntities);

            throw new ApiDataException(404, "No data found ", HttpStatusCode.NotFound);
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [AcceptVerbs("POST", "GetSpecOligosByFilter")]
        public HttpResponseMessage GetSpecOligosByFilter(List<Spec_Input> specInputs)
        {
            DataTable specOligos = _oligoServices.GetSpecOligosByFilter(specInputs);

            if (specOligos.Rows.Count > 0)
                return Request.CreateResponse(HttpStatusCode.OK, specOligos);

            throw new ApiDataException(404, "No data found for the input(s) ", HttpStatusCode.NotFound);
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [AcceptVerbs("POST", "GetSpecOligoById")]
        public HttpResponseMessage GetSpecOligoById(int id)
        {
            var oligo = _oligoServices.GetSpecOligoById(id);
            if (oligo != null)
                return Request.CreateResponse(HttpStatusCode.OK, oligo);

            throw new ApiDataException(404, "No oligo found for id "+id, HttpStatusCode.NotFound);
            throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
        }

        [AcceptVerbs("POST", "CreateSpecOligo")]
        public int CreateSpecOligo(Spec_OligoEntity specOligoEntity)
        {
            return _oligoServices.CreateSpecOligos(specOligoEntity);
        }

        // PUT api/product/5
        [PUT("Update/oligoid/{id}")]
        [PUT("Modify/oligoid/{id}")]
        public bool Put(int id, [FromBody] Spec_OligoEntity specOligoEntity)
        {
            if (id > 0)
            {
                return _oligoServices.UpdateSpecOligos(id, specOligoEntity);
            }
            return false;
        }

        [AcceptVerbs("POST", "CANCEL")]
        public bool CancelSpecOligo(List<int> ids)
        {
            if (ids.Count() > 0)
                return _oligoServices.CancelSpecOligo(ids);
            return false;
        }
    }
}
