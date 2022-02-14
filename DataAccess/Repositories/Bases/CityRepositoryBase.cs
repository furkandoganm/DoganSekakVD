using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class CityRepositoryBase: RepositoryBase<City>
    {
        public CityRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
