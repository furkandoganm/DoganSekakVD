using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class CategoryRepositoryBase: RepositoryBase<Category>
    {
        public CategoryRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
