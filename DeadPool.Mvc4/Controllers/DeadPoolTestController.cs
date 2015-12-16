using DeadPool.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeadPool.Mvc4.Controllers
{
    public class DeadPoolTestController : ApiController
    {
        // GET api/<controller>
        public ResultInstance Get(string id)
        {
            try
            {
                var test = DeadPoolService.Service.GetByKey(id);

                return new ResultInstance
                {
                    Result = test.Run(),
                    Key = test.Key,
                    Name = test.Name,
                    Description = test.Description
                };
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message));
            }
        }
    }

    [Serializable]
    public class ResultInstance
    {
        public TestResult Result { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}