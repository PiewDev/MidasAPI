using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbMappingAttribute : Attribute
    {
        public Type EntityType { get; }
        public DbMappingAttribute(Type dtoType)
        {
            EntityType = dtoType;
        }
        public static Type GetEntityClassFromDomain(Type domainType)
        {
            var entityClasses = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(DbMappingAttribute), false).Any())
                .ToList();

            foreach (var entityClass in entityClasses)
            {
                var attribute = (DbMappingAttribute)entityClass.GetCustomAttributes(typeof(DbMappingAttribute), false).First();
                if (attribute.EntityType == domainType)
                {
                    return entityClass;
                }
            }

            return null;
        }
        public static Type GetDbEntity<T>()
        {
            var domainType = typeof(T);
            return GetEntityClassFromDomain(domainType);
        }
    }
}