namespace Data;

public static class QueryableExtensions
{
    public static IQueryable<TSource> Page<TSource> (this IQueryable<TSource> queryable, int pageNumber, int pageSize)
    {
        var selection = (pageNumber - 1) * pageSize;
        if (selection < 0)
        {
            throw new InvalidOperationException("Negative page selection");
        }
        
        return queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static IEnumerable<TSource> Page<TSource>(this IOrderedEnumerable<TSource> queryable, int pageNumber, int pageSize)
    {
        var selection = (pageNumber - 1) * pageSize;
        if (selection < 0)
        {
            throw new InvalidOperationException("Negative page selection");
        }

        return queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}