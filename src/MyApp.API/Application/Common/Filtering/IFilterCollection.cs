using MyApp.Domain;

namespace MyApp.Application.Common.Filtering;

public interface IFilterCollection<TEntity, TFilter> where TEntity : BaseEntity
{
    IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);
}