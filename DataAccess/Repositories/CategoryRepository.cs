using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoryRepository: CategoryRepositoryBase
    {
        public CategoryRepository(DbContext db): base(db)
        {

        }
    }
}
