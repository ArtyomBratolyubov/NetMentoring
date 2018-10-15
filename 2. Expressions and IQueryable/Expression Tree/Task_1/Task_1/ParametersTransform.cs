using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Task_1
{
	public class ParametersTransform : ExpressionVisitor
	{
		public Dictionary<string, int> transformMap;

		public ParametersTransform(Dictionary<string, int> transformMap)
		{
			if (transformMap == null)
			{
				throw new ArgumentNullException("transformMap");
			}

			this.transformMap = transformMap;
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			if (this.transformMap.TryGetValue(node.Name, out int value))
			{
				return Expression.Constant(value);
			}

			return base.VisitParameter(node);
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			return Expression.Lambda(Visit(node.Body), node.Parameters);
		}
	}
}
