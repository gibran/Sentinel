using DeadPool.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeadPool.Mvc4.Controllers
{
    public class DeadPoolTestController : ApiController
    {
        public async Task<ResultInstance> Get(string id)
        {
            try
            {
                var test = DeadPoolService.Instance.GetByKey(id);
                await test.Run();

                return new ResultInstance
                {
                    Result = test.Result,
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
        public string Description { get; set; }
        public string Name { get; set; }
        public TestResult Result { get; set; }
    }
}