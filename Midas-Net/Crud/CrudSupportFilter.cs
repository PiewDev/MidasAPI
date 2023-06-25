using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Midas.Net.Domain.Crud;
using Midas.Net.ResponseHandling;
using System;
using System.Linq;
using System.Reflection;

namespace Midas.Net.Crud
{   
    public class CrudSupportFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var entityType = context.RouteData.Values["entityType"]?.ToString();

            Type targetType = GetEntityTypeByName(entityType);

            if (targetType == null || !HasCrudSupport(targetType))
            {
                throw new HttpException(404, 1, "Element not found");
            }

            var controller = context.Controller as CrudController;
            controller.EntityType= targetType;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        Type GetEntityTypeByName(string entityName)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            IEnumerable<Type> entityTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(CrudSupportAttribute), true).Any());

            return entityTypes.FirstOrDefault(type => type.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        }


        private bool HasCrudSupport(Type entityType)
        {
            bool hasCrudSupportAttribute = entityType.GetCustomAttributes(typeof(CrudSupportAttribute), true).Any();

            return hasCrudSupportAttribute;
        }
    }

}
