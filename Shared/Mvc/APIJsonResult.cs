using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mvc
{
    public class ApiJsonResult : ActionResult
    {
        private readonly object _data;
        private readonly HttpStatusCode _statusCode;


        public ApiJsonResult(object data) : this(data, HttpStatusCode.OK)
        {

        }

        public ApiJsonResult(object data, HttpStatusCode statusCode)
        {
            _statusCode = statusCode;

            Data = _data = data;
        }

        private static readonly string DefaultContentType = new MediaTypeHeaderValue("application/json")
        {
            Encoding = Encoding.UTF8
        }.ToString();

        public object Data { get; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var httpContext = context.HttpContext;
            var response = httpContext.Response;
            var request = httpContext.Request;
            var writerFactory = httpContext.RequestServices.GetRequiredService<IHttpResponseStreamWriterFactory>();
            var options = httpContext.RequestServices.GetRequiredService<IOptions<MvcJsonOptions>>().Value;
            var serializerSettings = options.SerializerSettings;

            response.StatusCode = (int)_statusCode;
            response.ContentType = DefaultContentType;
            using (var writer = writerFactory.CreateWriter(response.Body, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.CloseOutput = false;
                    var jsonSerializer = JsonSerializer.Create(serializerSettings);
                    //if (!request.Headers.TryGetValue("Multi-Language", out StringValues _))
                    //{
                    //    jsonSerializer.Converters.Add(new LocateStringConverter());
                    //}
                    jsonSerializer.Serialize(jsonWriter, _data);
                }
            }
            return Task.CompletedTask;

        }
    }
}
