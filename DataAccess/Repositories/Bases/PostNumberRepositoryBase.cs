using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class PostNumberRepositoryBase: RepositoryBase<PostNumber>
    {
        public PostNumberRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
