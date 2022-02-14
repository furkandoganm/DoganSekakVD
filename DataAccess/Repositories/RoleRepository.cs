using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class RoleRepository: RoleRepositoryBase
    {
        public RoleRepository(DbContext db): base(db)
        {

        }
    }
}
