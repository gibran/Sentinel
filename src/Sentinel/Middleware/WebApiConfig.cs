using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Sentinel.Middleware
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Configure(AutofacWebApiDependencyResolver resolver)
        {
            var configuration = new HttpConfiguration
            {
                DependencyResolver = resolver
            };

            configuration.MapHttpAttributeRoutes();
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.Remove(configuration.Formatters.FormUrlEncodedFormatter);
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            return configuration;
        }
    }
}