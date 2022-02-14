using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class UserRepositoryBase: RepositoryBase<User>
    {
        public UserRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
