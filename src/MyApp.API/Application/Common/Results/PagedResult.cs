using System.Collections.Generic;

namespace MyApp.Application.Common.Results;

public class PagedResult<T> : Result<IEnumerable<T>>
{
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    private PagedResult(IEnumerable<T> value, int count, int pageNumber, int pageSize, string error = "") 
        : base(value, true, error)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public static PagedResult<T> Create(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        => new(items, count, pageNumber, pageSize);

    public static new PagedResult<T> Fail(string error)
        => new(Enumerable.Empty<T>(), 0, 0, 0, error);
}