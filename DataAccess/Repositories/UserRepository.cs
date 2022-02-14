using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository: UserRepositoryBase
    {
        public UserRepository(DbContext db): base(db)
        {

        }
    }
}
