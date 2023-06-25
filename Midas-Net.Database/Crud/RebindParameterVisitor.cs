using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Crud
{
    public class RebindParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _newParameter;

        public RebindParameterVisitor(ParameterExpression newParameter)
        {
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type != _newParameter.Type)
                return base.VisitParameter(node);

            return _newParameter;
        }
    }
}
