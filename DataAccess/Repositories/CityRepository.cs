using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CityRepository : CityRepositoryBase
    {
        public CityRepository(DbContext db) : base(db)
        {

        }
    }
}
