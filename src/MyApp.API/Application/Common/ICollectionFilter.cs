namespace MyApp.Application.Common;

public interface ICollectionFilter<T>
    where T : PaginationFilter
{
    public T FilterData { get; init; }
}