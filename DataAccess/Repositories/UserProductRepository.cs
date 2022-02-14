using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserProductRepository: UserProductRepositoryBase
    {
        public UserProductRepository(DbContext db): base(db)
        {

        }
    }
}
