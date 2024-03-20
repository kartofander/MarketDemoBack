using Microsoft.EntityFrameworkCore;

namespace Data.Abstracts;

public abstract class DbQuery<T> : IQuery where T : class
{
    internal DbApplicationContext _dbContext;

    public DbQuery(DbApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    internal IQueryable<T> GetQueryableAsNoTracking()
    {
        return _dbContext
            .Set<T>()
            .AsQueryable()
            .AsNoTracking();
    }
}
