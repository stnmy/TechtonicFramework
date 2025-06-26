using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;

namespace TechtonicFramework.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Debug.WriteLine($"Exception: {context.Exception}");

            var response = new
            {
                message = "An error occurred.",
                exceptionMessage = context.Exception.Message,
                stackTrace = context.Exception.StackTrace,
                exceptionType = context.Exception.GetType().FullName
            };

            var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                JsonConvert.DeserializeObject(json)
            );
        }
    }
}
