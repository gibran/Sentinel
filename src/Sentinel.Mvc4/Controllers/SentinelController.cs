using Sentinel.Result;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sentinel.Mvc4.Controllers
{
    public class SentinelController : ApiController
    {
        [Route("api/sentinel/{id}")]
        public TestResult Get(string id)
        {
            try
            {
                return SentinelConfig.Instance.GetResultByTestName(id);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [Route("api/sentinel")]
        public IEnumerable<TestResult> Get()
        {
            try
            {
                return SentinelConfig.Instance.GetAllResults();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
            }
        }
    }
}