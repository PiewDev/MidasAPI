using System.Text;
using Midas.Net.Domain;
using Midas.Net.Domain.Log;
using log4net;
using Newtonsoft.Json;
using Midas.Net.Log;

namespace Midas.Net.ResponseHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(ErrorHandlingMiddleware));
        private readonly ILogService _logService;
        private string guid;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                guid = await _logService.SaveLog(await LogHelper.ToLog(context.Request));
                await _next.Invoke(context);
                string body;
                using (StreamReader stream = new StreamReader(context.Request.Body))
                {
                    body = await stream.ReadToEndAsync();
                }

                await _logService.SaveLog(context.Response.ToLog(guid, context, body));

                var requestData = Encoding.UTF8.GetBytes(body);
                context.Request.Body = new MemoryStream(requestData);
                context.Request.ContentLength = context.Request.Body.Length;

            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private void HandleException(HttpContext context, Exception e)
        {
            var response = context.Response;

            try
            {
                _logService.SaveLog(e.ToLog(guid,context));
            }
            finally
            {
                var customResponse = new Response();
                if (e is HttpException)
                {
                    logger.Error(string.Format("{0} - {1}", "*** Error controlado ***", e.ParseException()));

                    customResponse.Description = ((HttpException)e).Message;
                    response.StatusCode = ((HttpException)e).StatusCode;
                    response.ContentType = "application/json";
                    response.WriteAsync(JsonConvert.SerializeObject(customResponse));
                }
                else
                {
                    logger.Error(string.Format("{0} - {1}", "*** Error no controlado ***", e.ParseException()));

                    customResponse.Description = "Ha ocurrido un error inesperado, ya estamos trabajando en su solución, disculpe las molestias ocasionadas.";


                    response.StatusCode = 500;
                    response.ContentType = "application/json";
                    response.WriteAsync(JsonConvert.SerializeObject(customResponse));
                }
                _logService.SaveLog(context.Response.ToLog(guid, context, JsonConvert.SerializeObject(customResponse)));
            }
            //este mensaje de error se envía al cliente


        }



    }
}
