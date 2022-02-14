using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ReviewRepository: ReviewRepositoryBase
    {
        public ReviewRepository(DbContext db): base(db)
        {

        }
    }
}
