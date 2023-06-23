using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Midas.Net.ResponseHandling
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}
