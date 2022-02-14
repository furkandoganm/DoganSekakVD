using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class UserProductRepositoryBase: RepositoryBase<UserProduct>
    {
        public UserProductRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
