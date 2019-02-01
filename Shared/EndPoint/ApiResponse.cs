using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EndPoint
{
    public class ApiResponse<T>
    {
        /// <inheritdoc />
        public ApiResponse()
        {

        }

        public ApiResponse(T data)
        {
            if (data == null)
            {
                Successful = false;
            }
            else
            {
                Data = data;
                Successful = true;
            }
        }

        [JsonProperty("successful")]
        public bool Successful { get; set; }

        [JsonProperty("errorDescription")]
        public string ErrorDescription { get; set; }

        [JsonProperty("errorMessage")]
        public object ErrorMessage { get; set; }

        /// <summary>
        /// Response data, using T to serialize
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("log")]
        public string StackTrace { get; set; }

        [JsonProperty("errorCode")]
        public int? ErrorCode { get; set; }
    }
}
