using System.Reflection;
using System.Linq.Expressions;
using MyApp.Domain;

namespace MyApp.Application.Common.Filtering;

public class BaseFilterCollection<TEntity, TFilter> : IFilterCollection<TEntity, TFilter> 
    where TEntity : BaseEntity
{
    public IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter)
    {
        if (filter == null) return query;

        var filterProperties = typeof(TFilter).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        foreach (var filterProperty in filterProperties)
        {
            var value = filterProperty.GetValue(filter);
            if (value != null)
            {
                var entityProperty = typeof(TEntity).GetProperty(filterProperty.Name);
                if (entityProperty != null)
                {
                    query = ApplyPropertyFilter(query, entityProperty, value);
                }
            }
        }

        return query;
    }
    protected virtual IQueryable<TEntity> ApplyPropertyFilter(
    IQueryable<TEntity> query,
    PropertyInfo property,
    object value)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var memberExpression = Expression.Property(parameter, property);

        if (property.PropertyType == typeof(string) && value is string stringValue)
        {
            var method = typeof(string).GetMethod(
                nameof(string.Contains),
                new[] { typeof(string), typeof(StringComparison) });

            var constantValue = Expression.Constant(stringValue, typeof(string));
            var comparisonType = Expression.Constant(StringComparison.OrdinalIgnoreCase);
            var equalsExpression = Expression.Call(
                memberExpression,
                method!,
                constantValue,
                comparisonType);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalsExpression, parameter);
            return query.Where(lambda);
        }
        else
        {
            var constantExpression = Expression.Constant(value);
            var equalExpression = Expression.Equal(memberExpression, constantExpression);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);
            return query.Where(lambda);
        }
    }

}