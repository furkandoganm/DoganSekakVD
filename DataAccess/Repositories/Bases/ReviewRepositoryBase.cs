using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class ReviewRepositoryBase: RepositoryBase<Review>
    {
        public ReviewRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
