namespace Data.Abstracts;

public abstract class DbCommand<T> : ICommand where T : class
{
    internal DbApplicationContext _dbContext;

    public DbCommand(DbApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    internal IQueryable<T> GetQueryable()
    {
        return _dbContext
            .Set<T>()
            .AsQueryable();
    }
}
