using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class RoleRepositoryBase: RepositoryBase<Role>
    {
        public RoleRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
