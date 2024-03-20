using Data.Abstracts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Data.Queries;

public class UsersQuery : DbQuery<User>
{
    public UsersQuery(DbApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetUserByLogin(string login)
    {
        return await GetQueryableAsNoTracking()
            .SingleAsync<User>(u => u.Email == login);
    }

}
