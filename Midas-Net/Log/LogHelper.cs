using Midas.Net.Domain;
using Midas.Net.Domain.Log;
using System.Net;
using System.Text;

namespace Midas.Net.Log
{
    public static class LogHelper
    {
        public static HttpToLog ToLog(this HttpResponse res, string guid, HttpContext context, string body)
        {
            var type = context.Request.Method;
            var user = "HOSTNAME: " + context.Request.Host.Value + " - ADDRESS: " + $"{context.Request.Scheme}://{context.Request.Host}" + " - AGENT: " + (context.Request.Headers["User-Agent"].ToString());
            string headers = "";
            var statusCode = res.StatusCode != 0 ? ((HttpStatusCode)res.StatusCode) : HttpStatusCode.OK;
            var uri = context.Request.Path;
            var headersKeys = res.Headers?.Keys ?? new string[0];
            foreach (var key in headersKeys)
            {
                headers += key + " " + res.Headers[key] + "\n";
            }
            var bodyToSave = body;
            var logText = res.ParseResponse(body);


            return new HttpToLog() { Guid = guid, Body = bodyToSave, Uri = uri, Header = headers, StatusCode = statusCode, LogText = logText, Type = type, UserOrigin = user };
        }

        public static async Task<HttpToLog> ToLog(this HttpRequest req)
        {
            var uri = req.Path;
            var user = "HOSTNAME: " + req.Host.Value + " - ADDRESS: " + $"{req.Scheme}://{req.Host}" + " - AGENT: " + (req.Headers["User-Agent"].ToString());
            string headers = "";
            var headersKeys = req.Headers?.Keys ?? new string[0];
            foreach (var key in headersKeys)
            {
                headers += key + " " + req.Headers[key] + "\n";
            }
            var bodyToSave = RequestTypeHasBody(req.Method) ? await req.ReadRequestBodyAsync() : " - ";
            var requestType = req.Method;
            var logText = req.ParseRequest(bodyToSave);

            return new HttpToLog() { Guid = null, Body = bodyToSave, Header = headers, Type = requestType, Uri = uri, UserOrigin = user, LogText = logText };
        }

        public static HttpToLog ToLog(this Exception ex, string guid, HttpContext context)
        {
            var uri = context.Request.Path;
            var user = "HOSTNAME: " + context.Request.Host.Value + " - ADDRESS: " + $"{context.Request.Scheme}://{context.Request.Host}" + " - AGENT: " + (context.Request.Headers["User-Agent"].ToString());
            string headers = "";
            var headersKeys = context.Request.Headers?.Keys ?? new string[0];
            foreach (var key in headersKeys)
            {
                headers += key + " " + context.Request.Headers[key] + "\n";
            }
            var requestType = context.Request.Method;
            var logText = ex.ParseException();

            return new HttpToLog() { Guid = guid, Header = headers, Type = requestType, Uri = uri, UserOrigin = user, LogText = logText };
        }

        public static async Task<string> ReadRequestBodyAsync(this HttpRequest req)
        {
            req.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(req.ContentLength)];
            await req.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestContent = Encoding.UTF8.GetString(buffer);

            req.Body.Position = 0;  //rewinding the stream to 0
            return requestContent;
        }

        private static bool RequestTypeHasBody(string type)
        {
            return (type == "PUT" || type == "POST" || type == "PATCH");
        }


        public static string ParseRequest(this HttpRequest req, string body)
        {
            var uri = req.Path;
            var user = "HOSTNAME: " + req.Host.Value + " - ADDRESS: " + $"{req.Scheme}://{req.Host}" + " - AGENT: " + (req.Headers["User-Agent"].ToString());
            string headers = "";
            var headersKeys = req.Headers?.Keys ?? new string[0];
            foreach (var key in headersKeys)
            {
                headers += key + " " + req.Headers[key] + "\n";
            }
            var bodyToSave = RequestTypeHasBody(req.Method) ? body : " - ";
            var requestType = req.Method;

            // Agregar los parámetros de la URL
            var queryParams = req.Query;
            string paramsString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));

            return "REQUEST\nURI: " + uri + "\nUSER/ORIGIN: " + user + "\nHEADERS: " + headers + "\nTYPE: " + requestType + "\nPARAMS: " + paramsString + "\nBODY: " + bodyToSave + "\n";
        }

        public static string ParseResponse(this HttpResponse res, string body)
        {



            var statusCode = res.StatusCode != 0 ? ((HttpStatusCode)res.StatusCode) : HttpStatusCode.OK;
            var uri = " - ";
            var headersKeys = res.Headers.Keys ?? new string[0];
            string headers = "";
            foreach (var key in headersKeys)
            {
                headers += key + " " + res.Headers[key] + "\n";
            }
            var bodyToSave = body;

            return "RESPONSE\nURI: " + uri + "\nHEADERS: " + headers + "\nBODY: " + bodyToSave + "\nSTATUS_CODE: " + statusCode + "\n";

        }
        public static string ParseException(this Exception e)
        {
            try
            {
                string controlled = e is InternalException ? "CONTROLADA" : "NO CONTROLADA";
                string response = "";
                string data = "";
                foreach (var key in e.Data.Keys)
                {
                    data += key + " = " + e.Data[key] + "\n";
                }
                response += $"EXCEPTION {controlled} \n" +
                    "TARGET: " + e.TargetSite +
                    "\nSTACKTRACE: " + e.StackTrace +
                    "\nMESSAGE: " + e.ToString() +
                    "\nDATA: " + data +
                    "\nSOURCE: " + e.Source +
                    "\nHRESULT: " + e.HResult;

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Fallo ParseException(Exception e): \n" + ex.Message);

            }
        }
    }
}
