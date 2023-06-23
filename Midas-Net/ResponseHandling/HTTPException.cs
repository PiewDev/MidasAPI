using System;
using System.Net;

namespace Midas.Net.ResponseHandling
{  

    public class HttpException : Exception
    {
        public int StatusCode { get; }
        public HttpException(int statusCode,int errorCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
        public HttpException(HttpStatusCode exception) : base(exception.ToString())
        {
            StatusCode = (int)exception;
        }
    }
}
