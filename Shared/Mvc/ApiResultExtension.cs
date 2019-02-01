using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Shared.EndPoint;
using Shared.Exceptions;
using System.Net;
using System.Linq;


namespace Shared.Mvc
{
    public static class ApiResultExtension
    {
        public static IActionResult OkResult(this Controller controller)
        {
            return JsonResult(controller, new ApiResponse<object>() { Successful = true });
        }
        public static IActionResult OkResult(this Controller controller, bool successful)
        {
            return JsonResult(controller, new ApiResponse<object>() { Successful = successful });
        }
        public static IActionResult OkResult(this Controller controller, object data)
        {
            return JsonResult(controller, new ApiResponse<object>(data));
        }

        public static IActionResult CreatedResult(this Controller controller)
        {
            return JsonResult(controller, new ApiResponse<object>() { Successful = true }, HttpStatusCode.Created);
        }
        public static IActionResult JsonResult(this Controller controller, object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiJsonResult(obj, statusCode);
        }
        public static IActionResult ExceptionResult(this Controller controller, ErrorCode code)
        {
            return ExceptionResult(controller, code, HttpStatusCode.BadRequest);
        }
        public static IActionResult ExceptionResult(this Controller controller, ErrorCode code, HttpStatusCode statusCode)
        {
            return JsonResult(controller, new ApiResponse<object>()
            {
                ErrorDescription = code.ToString(),
                ErrorCode = (int)code,
                ErrorMessage = new { _message = Strings.Resource.ResourceManager.GetString("ERROR_CODE_" + code) }
            }, statusCode);
        }
        public static IActionResult ExceptionResult(this Controller controller, ModelStateDictionary modelState, object data)
        {
            var response = new ApiResponse<object>(data);
            if (!response.Successful)
            {
                var result = new JObject();
                var code = ErrorCode.INVALID_DATA;
                if (modelState.IsValid)
                {
                    return new OkObjectResult(response);
                }
                foreach (var invalid in modelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid))
                {
                    var message = invalid.Value.Errors.Select(x => x.ErrorMessage).Aggregate((x, y) => x + "\r\n" + y);
                    result.Add(invalid.Key, message);
                }
                result.Add("_message", Strings.Resource.ResourceManager.GetString("ERROR_CODE_" + code));
                response.ErrorCode = (int)code;
                response.ErrorDescription = code.ToString();
                response.ErrorMessage = result;

            }
            else
            {
                response.Data = data;
            }
            return JsonResult(controller, response, HttpStatusCode.BadRequest);
        }
    }
}
