using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class DistrictRepositoryBase: RepositoryBase<District>
    {
        public DistrictRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
