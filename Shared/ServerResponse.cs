using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ServerResponse<T>
    {
        [JsonConstructor]
        public ServerResponse(HttpStatusCode httpStatusCode, string message = "", T data = default)
        {
            Data = data;
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        public bool IsSuccess { 
            get
            {
                return 
                HttpStatusCode == HttpStatusCode.OK ||
                HttpStatusCode == HttpStatusCode.Created ||
                HttpStatusCode == HttpStatusCode.NotFound ||
                HttpStatusCode == HttpStatusCode.Accepted;
            }
        }              


        [JsonProperty("data")]
        public T Data { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("httpStatusCode")]
        public HttpStatusCode HttpStatusCode { get; private set; }
    }
}


