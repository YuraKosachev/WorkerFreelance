using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Freelance.Extensions
{

    // ------------------------------------------------------------------------------------------
    // This code was taken from the MSDN Blog meek: LINQ to Entities: Combining Predicates
    // http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
    // ------------------------------------------------------------------------------------------

    internal static class Utility
    {
        public static Expression<Func<T, bool>> Compose<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<Func<T, bool>>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
    }


    internal class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement)) { p = replacement; }
            return base.VisitParameter(p);
        }
    }


    internal static class ParameterReplacer
    {
        // Produces an expression identical to 'expression'
        // except with 'source' parameter replaced with 'target' parameter.     
        public static Expression<Func<TEntity, TResult>> Replace<TModel, TEntity, TResult>(this Expression<Func<TModel, TResult>> expression)
        {
            return new ParameterReplacerVisitor<TModel, TEntity, TResult>().VisitAndConvert(expression);
        }


        private class ParameterReplacerVisitor<TModel, TEntity, TResult> : ExpressionVisitor
        {
            private ParameterExpression source;
            private ParameterExpression target;

            internal Expression<Func<TEntity, TResult>> VisitAndConvert(Expression<Func<TModel, TResult>> root)
            {
                source = root.Parameters[0];
                target = Expression.Parameter(typeof(TEntity), source.Name);

                return (Expression<Func<TEntity, TResult>>)Visit(root);
            }

            /// <summary>
            /// Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression"/>.
            /// </summary>
            /// <returns>
            /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
            /// </returns>
            /// <param name="node">The expression to visit.</param>
            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Arguments.Any(expr => expr.NodeType == ExpressionType.Lambda))
                {
                    var sourceArguments = node.Arguments;
                    var sourceMethod = node.Method;

                    if (sourceMethod.ReflectedType == typeof(Enumerable))
                    {
                        MethodInfo methodInfo = typeof(Enumerable).GetMember(sourceMethod.Name).FirstOrDefault(memberInfo => memberInfo.MetadataToken == sourceMethod.MetadataToken) as MethodInfo;
                        if (methodInfo != null)
                        {
                            var targetArguments = VisitArguments(sourceArguments).ToList();
                            var targetMethod = methodInfo.MakeGenericMethod(targetArguments.First().Type.GenericTypeArguments);

                            return Expression.Call(targetMethod, targetArguments);
                        }
                    }
                }

                return base.VisitMethodCall(node);
            }

            private IEnumerable<Expression> VisitArguments(IList<Expression> arguments)
            {
                var instanceArgument = arguments.First();
                var newInstanceArgument = Visit(instanceArgument);

                yield return newInstanceArgument;

                var instanceType = instanceArgument.Type;
                var newInstanceType = newInstanceArgument.Type;

                var instanceElementType = instanceType.GenericTypeArguments[0];
                var newInstanceElementType = newInstanceType.GenericTypeArguments[0];

                var replaceMethod = typeof(ParameterReplacer).GetMethod("Replace");

                foreach (var expr in arguments.Skip(1))
                {
                    if (expr.NodeType != ExpressionType.Lambda)
                    {
                        yield return Visit(expr);
                    }

                    var resultType = ((LambdaExpression)expr).ReturnType;
                    var genericReplaceMethod = replaceMethod.MakeGenericMethod(instanceElementType, newInstanceElementType, resultType);

                    yield return (Expression)genericReplaceMethod.Invoke(null, new object[] { expr });
                }
            }

            protected override Expression VisitLambda<TDelegate>(Expression<TDelegate> node)
            {
                if (typeof(TDelegate) == typeof(Func<TModel, TResult>))
                {
                    // Leave all parameters alone except the one we want to replace.
                    var parameters = node.Parameters.Select(p => p == source ? target : p);

                    return Expression.Lambda<Func<TEntity, TResult>>(Visit(node.Body), parameters);
                }

                return base.VisitLambda(node);
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                // Replace the source with the target, visit other params as usual.
                return node == source ? target : base.VisitParameter(node);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Expression != null && node.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    var aggregator = VisitMember((MemberExpression)node.Expression);
                    var member = aggregator.Type.GetProperty(node.Member.Name);

                    return Expression.MakeMemberAccess(aggregator, member);
                }

                return node.Expression == source ? Expression.MakeMemberAccess(target, target.Type.GetProperty(node.Member.Name)) : base.VisitMember(node);
            }
        }
    }
}
