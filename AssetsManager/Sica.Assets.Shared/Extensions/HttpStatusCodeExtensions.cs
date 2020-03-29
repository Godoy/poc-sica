using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace Sica.Assets.Shared.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static HttpResponseMessage Result(this HttpStatusCode that, string message = null)
        {
            var response = new HttpResponseMessage(that);
            if (message != null)
                response.Content = new StringContent(message);
            return response;
        }

        public static HttpResponseMessage Result(this HttpStatusCode that, Exception exception)
        {
            var response = new HttpResponseMessage(that);
            if (exception != null)
                response.Content = new StringContent(exception.ToStringReccurent());
            return response;
        }

        public static HttpResponseMessage Result(this HttpStatusCode that, object obj)
        {
            var response = new HttpResponseMessage(that);
            if (obj != null)
                response.Content = new StringContent(JsonConvert.SerializeObject(obj));
            return response;
        }
    }
}
