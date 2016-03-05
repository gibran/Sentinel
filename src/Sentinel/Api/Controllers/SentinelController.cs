using Sentinel.Api.Filters;
using Sentinel.Core;
using Sentinel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sentinel.Api.Controllers
{
    [NoCache]
    [RoutePrefix("api")]
    public class SentinelController : ApiController
    {
        private ISentinelService SentinelService { get; }

        public SentinelController(ISentinelService sentinelService)
        {
            if (sentinelService == null) throw new ArgumentNullException(nameof(sentinelService));
            SentinelService = sentinelService;
        }

        [Route("sentinel/{id}")]
        public TestResult Get(string id)
        {
            try
            {
                return SentinelService.GetResultByTestName(id);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [Route("sentinel")]
        public IEnumerable<TestResult> Get()
        {
            try
            {
                return SentinelService.GetAllResults();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }
    }
}