using System.Linq.Expressions;

namespace PolarisLite.Web.Plugins;
public static class TypePropertiesNameResolver
{
    private static readonly string expressionCannotBeNullMessage = "The expression cannot be null.";
    private static readonly string invalidExpressionMessage = "Invalid expression.";

    public static string GetMemberName<T>(Expression<Func<T, object>> expression)
    where T : class
    {
        return GetMemberName(expression.Body);
    }

    public static List<string> GetMemberNames<T>(params Expression<Func<T, object>>[] expressions)
        where T : class
    {
        List<string> memberNames = new List<string>();
        foreach (var cExpression in expressions)
        {
            memberNames.Add(GetMemberName(cExpression.Body));
        }

        return memberNames;
    }

    private static string GetMemberName(Expression expression)
    {
        if (expression == null)
        {
            throw new ArgumentException(expressionCannotBeNullMessage);
        }

        if (expression is MemberExpression)
        {
            // Reference type property or field
            var memberExpression = (MemberExpression)expression;
            return memberExpression.Member.Name;
        }

        if (expression is MethodCallExpression)
        {
            // Reference type method
            var methodCallExpression = (MethodCallExpression)expression;
            return methodCallExpression.Method.Name;
        }

        if (expression is UnaryExpression)
        {
            // Property, field of method returning value type
            var unaryExpression = (UnaryExpression)expression;
            return GetMemberName(unaryExpression);
        }

        throw new ArgumentException(invalidExpressionMessage);
    }

    private static string GetMemberName(UnaryExpression unaryExpression)
    {
        if (unaryExpression.Operand is MethodCallExpression)
        {
            var methodExpression = (MethodCallExpression)unaryExpression.Operand;
            return methodExpression.Method.Name;
        }

        return ((MemberExpression)unaryExpression.Operand).Member.Name;
    }
}