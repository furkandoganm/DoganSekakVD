using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostNumberRepository: PostNumberRepositoryBase
    {
        public PostNumberRepository(DbContext db): base(db)
        {

        }
    }
}
