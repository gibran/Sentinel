using DeadPool.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeadPool.Mvc4.Controllers
{
    public class DeadPoolTestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<dynamic> Get()
        {
            var tests = DeadPoolService.Service.GetAll();

            return tests.Select(test => new ResultInstance {
                Result = test.Run(),
                Key = test.Key,
                Name = test.Name,
                Description = test.Description
            }).ToList();
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